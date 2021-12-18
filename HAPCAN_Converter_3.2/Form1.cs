using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAPCAN_Converter
{
    public partial class Form1 : Form
    {
        String filePath = string.Empty;

        public Form1()
        {
            InitializeComponent();
            Text = Application.ProductName;
        }

		private void button1_Click(object sender, EventArgs e)
        {
            //open hex file

            bool fileOK = false;
            string line, hardTypeString;
            Int32 hardType = 0, hardVer = 0, appType = 0, appVer = 0, firmVer = 0, firmRev = 0;

            //create open file dialog
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "MPLAB hex file (*.hex)|*.hex|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)                                 
                {
                    filePath = openFileDialog.FileName;                                             
                    toolStripStatusLabel1.Text = filePath;

                    //read the contents of the file
                    using (StreamReader reader = new StreamReader(filePath))                        
                    {
                        //read line by line
                        while ((line = reader.ReadLine()) != null)                                  
                        {
                            try
                            {
                                //get firmware declaration at 0x101000 address
                                if (line.Substring(3, 6) == "101000")                               
                                {
                                    hardType = Int32.Parse(line.Substring(9, 4), System.Globalization.NumberStyles.HexNumber);
                                    hardVer = Int32.Parse(line.Substring(13, 2), System.Globalization.NumberStyles.HexNumber);
                                    appType = Int32.Parse(line.Substring(15, 2), System.Globalization.NumberStyles.HexNumber);
                                    appVer = Int32.Parse(line.Substring(17, 2), System.Globalization.NumberStyles.HexNumber);
                                    firmVer = Int32.Parse(line.Substring(19, 2), System.Globalization.NumberStyles.HexNumber);
                                    firmRev = Int32.Parse(line.Substring(21, 4), System.Globalization.NumberStyles.HexNumber);
                                    fileOK = true;
                                    break;
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }

                    //check if this is a HAPCAN file	
                    if (fileOK)
                    {
                        //check if this is a UNIV firmware
                        if (hardType == 0x3000)
                        {
                            hardTypeString = "UNIV";
                        }
                        else
                        {
                            hardTypeString = "0x" + hardType.ToString("X4");
                        }
                        //display opened file
                        richTextBox1.AppendText("______________________________________________________________________" + System.Environment.NewLine);
                        richTextBox1.AppendText("OPEN FILE                                    Time: " + DateTime.Now + System.Environment.NewLine);
                        richTextBox1.AppendText(System.Environment.NewLine);
                        richTextBox1.AppendText("                  File: " + filePath + System.Environment.NewLine);
                        richTextBox1.AppendText("              Firmware: " + hardTypeString + " " + hardVer + "." + appType + "." + appVer + "." + firmVer + " rev." + firmRev + System.Environment.NewLine);
                        richTextBox1.AppendText("         Hardware Type: " + hardTypeString+ " (0x" + hardType.ToString("X4") + ")" + System.Environment.NewLine);
                        richTextBox1.AppendText("      Hardware Version: " + hardVer + System.Environment.NewLine);
                        richTextBox1.AppendText("      Application Type: " + appType + System.Environment.NewLine);
                        richTextBox1.AppendText("   Application Version: " + appVer + System.Environment.NewLine);
                        richTextBox1.AppendText("      Firmware Version: " + firmVer + System.Environment.NewLine);
                        richTextBox1.AppendText("     Firmware Revision: " + firmRev + System.Environment.NewLine);
                        richTextBox1.ScrollToCaret();
                        button2.Enabled = true;
                    }
                    else
                    {   
                        //display wrong file
                        MessageBox.Show("This is not a valid HAPCAN firmware file", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        richTextBox1.AppendText("______________________________________________________________________" + System.Environment.NewLine);
                        richTextBox1.AppendText("WRONG FILE                                   Time: " + DateTime.Now + System.Environment.NewLine);
                        richTextBox1.AppendText(System.Environment.NewLine);
                        richTextBox1.AppendText("      File: " + filePath + System.Environment.NewLine);
                        richTextBox1.AppendText("            This is not a valid HAPCAN firmware file" + System.Environment.NewLine);
                        richTextBox1.ScrollToCaret();
                        button2.Enabled = false;
                    }
                    
                }
            }
        }

		private void button2_Click(object sender, EventArgs e)
		{
			//convert hex file to haf file

            String hStartCode, hafFileLine, hafFile, hafFileName, line, hardTypeString; ;
			Int32 hByteCount, hAddress, hRecordType, hCheckSum, hAddressMax=0;
			Byte hByteValue = 0xFF;
			bool fileOK = false;

            //create buffer of hex and fill it
            Byte[] HexBuffer = new Byte[0x10000];                               
			for (Int32 i = 0; i < 0x10000; i++)
				HexBuffer[i] = 0xFF;

			//read file to buffer line by line
            using (StreamReader reader = new StreamReader(filePath))
			{
				while ((line = reader.ReadLine()) != null)
				{
                    //read header of line		 
					try
					{
						hStartCode = line.Substring(0, 1);
						hByteCount = Int32.Parse(line.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
						hAddress = Int32.Parse(line.Substring(3, 4), System.Globalization.NumberStyles.HexNumber);
						hRecordType = Int32.Parse(line.Substring(7, 2), System.Globalization.NumberStyles.HexNumber);
                        fileOK = true;
                    }
					catch (Exception)
                    {
                        fileOK = false;
                        break;
                    }

					//read data bytes of line
					if (fileOK == true && hStartCode == ":" && hByteCount <= 0x10 && hAddress < 0x10000 && hRecordType == 0x00)
					{
						for (Int32 i = 0; i < hByteCount; i++)
						{
							try
							{
								hByteValue = Byte.Parse(line.Substring(9 + 2 * i, 2), System.Globalization.NumberStyles.HexNumber);
							}
							catch (Exception)
							{
                                //update address to display error
                                hAddressMax = hAddress + i;
                                fileOK = false;
                                //exit 'for' loop
                                break;         
							}
                            //update register
                            HexBuffer[hAddress + i] = hByteValue;
                            //update max address
                            if (hAddressMax < hAddress + i)                         
                                hAddressMax = hAddress + i;
                        }
                        //stop reading if file error
                        if (fileOK == false)
							break;                                                      	
					}
				}
			}

            //display if error in file
            if (fileOK == false)
            {
                MessageBox.Show("File reading error", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                richTextBox1.AppendText("______________________________________________________________________" + System.Environment.NewLine);
                richTextBox1.AppendText("FILE ERROR                                   Time: " + DateTime.Now + System.Environment.NewLine);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText("      File error at address 0x" + hAddressMax.ToString("X6") + System.Environment.NewLine);
                richTextBox1.ScrollToCaret();
            }
            //create haf file
            else
            {
                //count file checksum
                hCheckSum = 0;
                for (Int32 i = 0x1020; i < 0x8000; i++)
                    hCheckSum += HexBuffer[i];
                HexBuffer[0x1000] = (Byte)(hCheckSum >> 16);
                HexBuffer[0x1001] = (Byte)(hCheckSum >> 8);
                HexBuffer[0x1002] = (Byte)hCheckSum;

                //header
                hafFile = "<--- HAPCAN - Home Automation Project ---->" + System.Environment.NewLine;
                hafFile += "<---------- website: hapcan.com ---------->" + System.Environment.NewLine;
                if (HexBuffer[0x1010] == 0x30 && HexBuffer[0x1011] == 0x00)          //UNIV processor?
                    hardTypeString = "UNIV";
                else
                    hardTypeString = "0x" + HexBuffer[0x1010].ToString("X2")+ HexBuffer[0x1011].ToString("X2");
                hafFileLine = "<      Firmware: " + hardTypeString + " " + HexBuffer[0x1012] + "." + HexBuffer[0x1013] + "." + HexBuffer[0x1014] + "." + HexBuffer[0x1015] + " rev." + (HexBuffer[0x1016] * 256 + HexBuffer[0x1017]) + "       >";
                hafFileLine = hafFileLine.Remove(1, (hafFileLine.Length - 43) / 2);
                hafFileLine = hafFileLine.Remove(41, hafFileLine.Length - 43);
                hafFile += hafFileLine + System.Environment.NewLine;

                //data bytes
                for (Int32 i = 0x1000; i < 0x10000; i += 16)
                {
                    //line header
                    hafFileLine = ":10" + i.ToString("X4") + "00";                          
                    Byte LineCheckSum = (Byte)(0x10 + (i >> 8) + (i));
                    
                    //line bytes
                    for (Int32 j = 0; j < 16; j++)                                          
                    {
                        hafFileLine += HexBuffer[i + j].ToString("X2");
                        LineCheckSum += HexBuffer[i + j];
                    }
                    
                    //line checksum
                    LineCheckSum = (Byte)((~LineCheckSum) + 1);                             
                    hafFileLine += LineCheckSum.ToString("X2") + System.Environment.NewLine;
                    
                    //add line to file if data not equal 0xFF
                    if (hafFileLine.Substring(9, 32) != "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF")    
                        hafFile += hafFileLine;
                }

                //save haf file
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "HAPCAN haf file (*.haf)|*.haf|All files (*.*)|*.*";
                    hafFileName = hardTypeString.ToLower() + "_" + HexBuffer[0x1012] + "-" + HexBuffer[0x1013] + "-" + HexBuffer[0x1014] + "-" + HexBuffer[0x1015] + "-rev" + (HexBuffer[0x1016] * 256 + HexBuffer[0x1017]);
                    saveFileDialog.FileName = Path.GetFileName(hafFileName);
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                        {
                            writer.Write(hafFile);
                        }
                        richTextBox1.AppendText("______________________________________________________________________" + System.Environment.NewLine);
                        richTextBox1.AppendText("CONVERTED FILE SAVED TO                      Time: " + DateTime.Now + System.Environment.NewLine);
                        richTextBox1.AppendText(System.Environment.NewLine);
                        richTextBox1.AppendText("                  File: " + saveFileDialog.FileName + System.Environment.NewLine);
                        richTextBox1.AppendText("         File Checksum: 0x" + hCheckSum.ToString("X6") + System.Environment.NewLine);
                        richTextBox1.AppendText("     File Last Address: 0x" + hAddressMax.ToString("X6") + System.Environment.NewLine);
                        richTextBox1.ScrollToCaret();
                    } 
                }
            }
        }

    }
}
