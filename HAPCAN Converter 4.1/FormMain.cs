namespace HAPCAN_Converter;

public partial class FormMain : FormBase
{
    string _filePath = "";
    string _hafFileName = "";
    string _processor = "";
    

    public FormMain()
    {
        InitializeComponent();
        Text = Application.ProductName;
        base.labelTitle.Text = Application.ProductName;
    }

    private void buttonOpenFile_Click(object sender, EventArgs e)
    {

        bool fileOK = false;
        string line, hardTypeString;
        int hardType = 0, hardVer = 0, appType = 0, appVer = 0, firmVer = 0, firmRev = 0;

        //create open file dialog
        using var openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "MPLAB hex file (*.hex)|*.hex|All files (*.*)|*.*";

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            _filePath = openFileDialog.FileName;
            label3.Text = _filePath;

            //read the contents of the file
            try
            {
                using var reader = new StreamReader(_filePath);

                //read line by line
                while ((line = reader.ReadLine()) != null)
                {
                    //if UNIV3, then get firmware declaration at 0x1010 address
                    if (line.Substring(3, 6) == "101000")
                    {
                        hardType = Int32.Parse(line.Substring(9, 4), System.Globalization.NumberStyles.HexNumber);
                        hardVer = Int32.Parse(line.Substring(13, 2), System.Globalization.NumberStyles.HexNumber);
                        appType = Int32.Parse(line.Substring(15, 2), System.Globalization.NumberStyles.HexNumber);
                        appVer = Int32.Parse(line.Substring(17, 2), System.Globalization.NumberStyles.HexNumber);
                        firmVer = Int32.Parse(line.Substring(19, 2), System.Globalization.NumberStyles.HexNumber);
                        firmRev = Int32.Parse(line.Substring(21, 4), System.Globalization.NumberStyles.HexNumber);
                        //hardType = 0xFFFF means that it is not for PIC18F26K80
                        if (hardType != 0xFFFF && hardVer == 3)
                        {
                            fileOK = true;
                            _processor = "PIC18F26K80";
                            break;
                        }
                    }
                    //if UNIV4, then get firmware declaration at 0x2010 address
                    if (line.Substring(3, 6) == "201000")
                    {
                        hardType = Int32.Parse(line.Substring(9, 4), System.Globalization.NumberStyles.HexNumber);
                        hardVer = Int32.Parse(line.Substring(13, 2), System.Globalization.NumberStyles.HexNumber);
                        appType = Int32.Parse(line.Substring(15, 2), System.Globalization.NumberStyles.HexNumber);
                        appVer = Int32.Parse(line.Substring(17, 2), System.Globalization.NumberStyles.HexNumber);
                        firmVer = Int32.Parse(line.Substring(19, 2), System.Globalization.NumberStyles.HexNumber);
                        firmRev = Int32.Parse(line.Substring(21, 4), System.Globalization.NumberStyles.HexNumber);
                        //hardType = 0xFFFF means that it is not for PIC18F27Q83
                        if (hardType != 0xFFFF && hardVer == 4)
                        {
                            fileOK = true;
                            _processor = "PIC18F27Q83";
                            break;
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
                    //create file name
                    _hafFileName = $"{hardTypeString}_{hardVer}-{appType}-{appVer}-{firmVer}-rev{firmRev}";
                    //display opened file
                    DisplayFileInfo(_filePath, _processor, hardTypeString, hardType, hardVer, appType, appVer, firmVer, firmRev);
                    buttonConvert.Enabled = true;
                }
                else
                {
                    //display wrong file
                    DisplayWrongFile(_filePath);
                    buttonConvert.Enabled = false;
                }
            }
            catch (Exception ex)
            {   //display error
                DisplayException(ex);
            }
        }
    }
    void DisplayException(Exception ex)
    {
        richTextBox1.AppendText("__________________________________________________________________________________________" + System.Environment.NewLine);
        richTextBox1.AppendText("OPEN FILE ERROR                                                  Time: " + DateTime.Now + System.Environment.NewLine);
        richTextBox1.AppendText(System.Environment.NewLine);
        richTextBox1.AppendText("      " + ex.Message + System.Environment.NewLine);
        richTextBox1.ScrollToCaret();
    }
    void DisplayWrongFile(string filePath)
    {
        richTextBox1.AppendText("__________________________________________________________________________________________" + System.Environment.NewLine);
        richTextBox1.AppendText("WRONG FILE                                                       Time: " + DateTime.Now + System.Environment.NewLine);
        richTextBox1.AppendText(System.Environment.NewLine);
        richTextBox1.AppendText("      File: " + filePath + System.Environment.NewLine);
        richTextBox1.AppendText("      This is not a valid HAPCAN firmware file" + System.Environment.NewLine);
        richTextBox1.ScrollToCaret();
    }
    void DisplayFileInfo(string filePath, string processor, string hardTypeString, int hardType, int hardVer, int appType, int appVer, int firmVer, int firmRev)
    {
        richTextBox1.AppendText("__________________________________________________________________________________________" + System.Environment.NewLine);
        richTextBox1.AppendText("OPEN FILE                                                        Time: " + DateTime.Now + System.Environment.NewLine);
        richTextBox1.AppendText(System.Environment.NewLine);
        richTextBox1.AppendText("                  File: " + filePath + System.Environment.NewLine);
        richTextBox1.AppendText("             Processor: " + processor + System.Environment.NewLine);
        richTextBox1.AppendText("              Firmware: " + hardTypeString + " " + hardVer + "." + appType + "." + appVer + "." + firmVer + " rev." + firmRev + System.Environment.NewLine);
        richTextBox1.AppendText("         Hardware Type: 0x" + hardType.ToString("X4") + " (" + hardTypeString + ")" + System.Environment.NewLine);
        richTextBox1.AppendText("      Hardware Version: " + hardVer + System.Environment.NewLine);
        richTextBox1.AppendText("      Application Type: " + appType + System.Environment.NewLine);
        richTextBox1.AppendText("   Application Version: " + appVer + System.Environment.NewLine);
        richTextBox1.AppendText("      Firmware Version: " + firmVer + System.Environment.NewLine);
        richTextBox1.AppendText("     Firmware Revision: " + firmRev + System.Environment.NewLine);
        richTextBox1.ScrollToCaret();
    }


    private void buttonConvert_Click(object sender, EventArgs e)
    {
        string hafFile = "";
        int hafFileChecksum = 0;
        int hafFileAddressMax = 0;

        try
        {
            //convert to haf file
            if (_processor == "Univ3Type")
            {
                (hafFile, hafFileChecksum, hafFileAddressMax) = Convert.ConvertFileUniv3Type(_filePath);
            }
            if (_processor == "Univ4Type")
            {
                (hafFile, hafFileChecksum, hafFileAddressMax) = Convert.ConvertFileUniv4Type(_filePath);
            }

            //save haf file
            using var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "HAPCAN haf file (*.haf)|*.haf|All files (*.*)|*.*";
            saveFileDialog.FileName = Path.GetFileName(_hafFileName);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using var writer = new StreamWriter(saveFileDialog.FileName);
                writer.Write(hafFile);
                DisplayFileSaved(saveFileDialog.FileName, hafFileChecksum, hafFileAddressMax);
            }
        }
        catch (FileFormatException ex)
        {   //display error
            DisplayFileError(ex.Message);
        }
        catch(Exception ex)
        {   //display error
            DisplayException(ex);
        }
    }

    void DisplayFileError(string message)
    {
        richTextBox1.AppendText("__________________________________________________________________________________________" + System.Environment.NewLine);
        richTextBox1.AppendText("FILE ERROR                                                       Time: " + DateTime.Now + System.Environment.NewLine);
        richTextBox1.AppendText(System.Environment.NewLine);
        richTextBox1.AppendText("      " + message + System.Environment.NewLine);
        richTextBox1.ScrollToCaret();
    }
    void DisplayFileSaved(string filename, int hCheckSum, int hAddressMax)
    {
        richTextBox1.AppendText("__________________________________________________________________________________________" + System.Environment.NewLine);
        richTextBox1.AppendText("CONVERTED FILE SAVED TO                                          Time: " + DateTime.Now + System.Environment.NewLine);
        richTextBox1.AppendText(System.Environment.NewLine);
        richTextBox1.AppendText("                  File: " + filename + System.Environment.NewLine);
        richTextBox1.AppendText("         File Checksum: 0x" + hCheckSum.ToString("X6") + System.Environment.NewLine);
        richTextBox1.AppendText("     File Last Address: 0x" + hAddressMax.ToString("X6") + System.Environment.NewLine);
        richTextBox1.ScrollToCaret();
    }
}
