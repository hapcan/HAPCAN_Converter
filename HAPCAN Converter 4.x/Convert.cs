using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAPCAN_Converter;

internal class Convert
{
    static int _hCheckSum;
    static int _hAddressMax;

    internal static (string, int, int) ConvertFileUniv3Type(string filePath)
    {
        var hexBuf = ReadHexFile(filePath, 0x10000);
        var hafFile = CreateHafFile(hexBuf, 0x1000, 0x8000);
        return (hafFile, _hCheckSum, _hAddressMax);
    }
    internal static (string, int, int) ConvertFileUniv4Type(string filePath)
    {
        var hexBuf = ReadHexFile(filePath, 0x20000);
        var hafFile = CreateHafFile(hexBuf, 0x2000, 0x10000);
        return (hafFile, _hCheckSum, _hAddressMax);
    }

    internal static byte[] ReadHexFile(string filePath, int bufferSize)
    {
        string hStartCode, line;
        int lineNo = 0, hByteCount, hAddress, hRecordType, hExtendedAddress = 0;
        byte hByteValue;

        //create buffer of hex and fill it
        byte[] HexBuffer = new byte[bufferSize];
        for (int i = 0; i < bufferSize; i++)
            HexBuffer[i] = 0xFF;

        //read file to buffer line by line
        try
        {
            using var reader = new StreamReader(filePath);

            while ((line = reader.ReadLine()) != null)
            {
                lineNo++;

                //read header of line		 
                try
                {
                    hStartCode = line.Substring(0, 1);
                    hByteCount = Int32.Parse(line.Substring(1, 2), System.Globalization.NumberStyles.HexNumber); 
                    hAddress = Int32.Parse(line.Substring(3, 4), System.Globalization.NumberStyles.HexNumber);
                    hRecordType = Int32.Parse(line.Substring(7, 2), System.Globalization.NumberStyles.HexNumber);
                }
                catch (Exception)
                {
                    throw new FileFormatException($"Error at address 0x{_hAddressMax:X6}, line: {lineNo}");
                }

                //Extended Linear Address
                if (hStartCode == ":" && hByteCount == 0x02 && hAddress == 0x0000 && hRecordType == 0x04)
                {
                    try
                    {
                        var baseAddress = Byte.Parse(line.Substring(9, 4), System.Globalization.NumberStyles.HexNumber);
                        hExtendedAddress = baseAddress * 256 * 256;
                    }
                    catch (Exception)
                    {
                        throw new FileFormatException($"Extended address error '{line}', line: {lineNo}");
                    }
                }

                //read data bytes of line
                if (hStartCode == ":" && hByteCount <= 0x10 && hAddress < 0x10000 && hRecordType == 0x00)
                {
                    for (int i = 0; i < hByteCount; i++)
                    {
                        try
                        {
                            hByteValue = Byte.Parse(line.Substring(9 + 2 * i, 2), System.Globalization.NumberStyles.HexNumber);
                        }
                        catch (Exception)
                        {
                            //update address to display error
                            _hAddressMax = hExtendedAddress + hAddress + i;
                            throw new FileFormatException($"Error at address 0x{_hAddressMax:X6}, line: {lineNo}");
                        }
                        //get only data up to address of buffer size
                        if (hExtendedAddress + hAddress + i < bufferSize)
                        {
                            //update register
                            HexBuffer[hExtendedAddress + hAddress + i] = hByteValue;
                            //update max address
                            if (_hAddressMax < hExtendedAddress + hAddress + i)
                                _hAddressMax = hExtendedAddress + hAddress + i;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new FileFormatException("Firmware file reading error. " + ex.Message);
        }
        return HexBuffer; 
    }

    internal static string CreateHafFile(byte[] hexBuffer, int adrFrom, int adrTo)
    {
        string hafFileLine, hafFile, hardTypeString;

        //count file checksum
        _hCheckSum = 0;
        for (int i = adrFrom + 0x20; i < adrTo; i++)            //calculate all byte starting from adrFrom + 0x20
            _hCheckSum += hexBuffer[i];
        hexBuffer[adrFrom + 0] = (Byte)(_hCheckSum >> 16);      //place checksum at address adrFrom
        hexBuffer[adrFrom + 1] = (Byte)(_hCheckSum >> 8);
        hexBuffer[adrFrom + 2] = (Byte)_hCheckSum;

        //header
        hafFile =  "<--- HAPCAN - Home Automation Project ---->" + System.Environment.NewLine;
        hafFile += "<---------- website: hapcan.com ---------->" + System.Environment.NewLine;
        if (hexBuffer[adrFrom + 0x10] == 0x30 && hexBuffer[adrFrom + 0x11] == 0x00)          //UNIV processor?
            hardTypeString = "UNIV";
        else
            hardTypeString = $"0x{hexBuffer[adrFrom + 0x10].ToString("X2")}{hexBuffer[adrFrom + 0x11].ToString("X2")}";
        hafFileLine = $"<      Firmware: {hardTypeString} " +
            $"{hexBuffer[adrFrom + 0x12]}.{hexBuffer[adrFrom + 0x13]}." +
            $"{hexBuffer[adrFrom + 0x14]}.{hexBuffer[0x1015]} " +
            $"rev.{hexBuffer[adrFrom + 0x16] * 256 + hexBuffer[adrFrom + 0x17]}       >";
        hafFileLine = hafFileLine.Remove(1, (hafFileLine.Length - 43) / 2);                 //centre the string
        hafFileLine = hafFileLine.Remove(41, hafFileLine.Length - 43);
        hafFile += hafFileLine + System.Environment.NewLine;

        //data bytes
        for (int i = adrFrom; i < adrTo; i += 16)
        {
            //add extended address line
            if (i == adrFrom)
                hafFile += ":020000040000FA" + System.Environment.NewLine;
            else if (i == 0x10000)
                hafFile += ":020000040001FB" + System.Environment.NewLine;

            //line header
            hafFileLine = $":10{i:X4}00";
            byte LineCheckSum = (byte)(0x10 + (i >> 8) + (i));

            //line bytes
            for (int j = 0; j < 16; j++)
            {
                hafFileLine += $"{hexBuffer[i + j]:X2}";
                LineCheckSum += hexBuffer[i + j];
            }

            //line checksum
            LineCheckSum = (byte)((~LineCheckSum) + 1);
            hafFileLine += LineCheckSum.ToString("X2") + System.Environment.NewLine;

            //add line to file if data not equal 0xFF
            if (hafFileLine.Substring(9, 32) != "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF")
                hafFile += hafFileLine;
        }

        //add end of file 
        hafFile += ":00000001FF";

        return hafFile;
    }
}
