using System;
using System.Drawing;
using System.IO;

namespace ConvertToBlackWhite
{
    public class Converter
    {
        public Converter() { }
        public void FromFile(Bitmap input, string outputFile)
        {
            Bitmap converted;
            try
            {
                converted = new Bitmap(input.Width, input.Height);
            } catch(Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
            for (int i = 0; i < input.Width; i++)
            {
                for (int j = 0; j < input.Width; j++)
                {
                    Color pixelColor = input.GetPixel(i, j);
                    if (pixelColor.GetBrightness() > 0.90) 
                    {
                        converted.SetPixel(i, j, Color.White);
                    } else
                    {
                        converted.SetPixel(i, j, Color.Black);
                    }
                }
            }
            try
            {
                converted.Save(outputFile);
            } catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        public void FromDirectory(string inputFolder, string outputFolder, string searchPattern)
        {
            var dir = new DirectoryInfo(inputFolder);
            var files = dir.GetFiles(searchPattern);

            foreach (var file in files)
            {
                var input = new Bitmap(file.FullName);
                FromFile(input, outputFolder + file.Name);
            }
        }

    }
}
