using iTextSharp.text;
using iTextSharp.text.pdf;
using Rules.Character;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CthulhuGen
{
    public class PdfAdapter
    {
        private const string PDF_SOURCE = "bogen.pdf";


        public static void WriteCharacter(CharacterItem character, string targetFilename)
        {
            Dictionary<string, string> mapping = GetFieldMapping(PDF_SOURCE);

            PdfReader.unethicalreading = true;      //will only fill forms. I promise!
            PdfReader reader = new PdfReader(PDF_SOURCE);
            PdfStamper stamper = new PdfStamper(reader, File.Create(targetFilename));
            
            foreach (var field in stamper.AcroFields.Fields)
            {
                string key = field.Key;
                string characterKey = mapping.ContainsKey(key) ? mapping[key] : key;

                int value = character.GetNumericalValue(characterKey);
                if (value >= 0)
                {
                    stamper.AcroFields.SetField(key, value.ToString());
                    stamper.AcroFields.RegenerateField(key);
                }
            }

            
            stamper.AddJavaScript("calc", "this.calculateNow();");
            stamper.Close();
        }

        private static Dictionary<string, string> GetFieldMapping(string pdfFilename)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            using (var reader = File.OpenText(Path.ChangeExtension(pdfFilename, "csv")))
            {
                string line = reader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    string[] token = line.Split(',');
                    result[token[0]] = token[1];

                    line = reader.ReadLine();
                }
            }

            return result;
        }
    }
}
