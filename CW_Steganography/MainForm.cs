using System;
using System.Drawing.Imaging;
using System.Text;

namespace Stenography;
public partial class MainForm : Form
{
    Image ActiveImage; // The selected image that we are working with
    Bitmap pictBmp; // For working with image pixels

    public MainForm()
    {
        InitializeComponent();
    }

    private void bt_choosePict_Click(object sender, EventArgs e) // Selecting an image and displaying it on the screen
    {
        using (OpenFileDialog dialog = new OpenFileDialog())
        {
            dialog.Filter = "Зображення:|*.jpg;*.jpeg;*.bmp;*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream filestrm = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    ActiveImage = Image.FromStream(filestrm);
                    pb_activePhoto.Image = ActiveImage; // Output to the screen

                    bt_encoding.Enabled = true; // Activate the decoding button

                    tb_codingText.Enabled = true; // Activate the text field
                }
            }
        }
    }

    private void SetNewColor(int width, int height, ref string bitString) // Iterate over all pixels of the picture until we change the last bits of each color to the value from bitString
    {        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color pixelColor = pictBmp.GetPixel(x, y); // Load the pixel
                string[] RGB = new string[3]; // Create an array for the values of three shades of colors
                // We write the pixel values in the array, adding zeros at the beginning to 8 lengths
                RGB[0] = Convert.ToString(pixelColor.R, 2).PadLeft(8, '0');
                RGB[1] = Convert.ToString(pixelColor.G, 2).PadLeft(8, '0');
                RGB[2] = Convert.ToString(pixelColor.B, 2).PadLeft(8, '0');

                for (int i = 0; i < 3; i++) // Iterate through the array (using for through using ref in the method)
                {
                    if (bitString.Length == 0)
                    {
                        return;
                    }
                    else ChangeColor(ref bitString, ref RGB[i]);
                }
                pictBmp.SetPixel(x, y, Color.FromArgb(Convert.ToInt32(RGB[0], 2), Convert.ToInt32(RGB[1], 2), Convert.ToInt32(RGB[2], 2))); // Set the new pixel color value
            }
        }
    }

    private void bt_coding_Click(object sender, EventArgs e) // Encoding the text into an image
    {
        string bitString = GetStringOfBinaryCode(); // 00000000 + usertext(every number 16 symbols) + 00000000
        pictBmp = new Bitmap(ActiveImage);
        int width = pictBmp.Width;
        int height = pictBmp.Height;

        if (width * height * 3 < bitString.Length) // If the number of pixels is not enough to encode
        {
            MessageBox.Show($"Зображення має недостатньо пікселів для кодування тексту!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        else
        {
            SetNewColor(width, height, ref bitString);
        }

        DialogResult dr = MessageBox.Show($"Чи бажаєте зберегти результат?", "Текст успішно закодовано", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (dr == DialogResult.Yes)
        {
            using (SaveFileDialog dialog = new SaveFileDialog()) 
            {
                dialog.Filter = "Зображення:|*.jpg;*.jpeg;*.bmp;*.png";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream fileStream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write))
                    {
                        pictBmp.Save(fileStream, ImageFormat.Png);
                    }
                }
            }
        }
        dr = MessageBox.Show($"Чи бажаєте працювати з новим зображенням?", "Вибір зображення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (dr == DialogResult.Yes)
        {
            pb_activePhoto.Image = pictBmp;
            ActiveImage = pictBmp;
        }

    }

    private string GetStringOfBinaryCode() // Create a string with text to encode in binary
    {
        EncodingProvider provider = CodePagesEncodingProvider.Instance;
        Encoding.RegisterProvider(provider);
        // Obtaining numerical equivalents of characters in the text for encoding
        byte[] bytes = Encoding.GetEncoding(1251).GetBytes(tb_codingText.Text);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 16; i++) sb.Append('0'); // When reading, will mean the beginning and the end
        for (int i = 0; i < bytes.Length; i++) sb.Append(Convert.ToString(bytes[i], 2).PadLeft(16, '0'));
        for (int i = 0; i < 16; i++) sb.Append('0');

        return sb.ToString();
    }


    private void ChangeColor(ref string bitString, ref string color)
    {
        // Removing the last bit from the color in binary format
        color = color.Remove(color.Length - 1); //00000000 - ..0

        // Write down the bit value from the line where the text is presented in binary form
        color += bitString[0];
        bitString = bitString.Remove(0, 1);
    }

    private void ReadLastBits(int width, int height, ref string decodeString, StringBuilder decodePix)
    {
        bool startSymbols = false;
        for (int x = 0; x < pictBmp.Width; x++)
        {
            for (int y = 0; y < pictBmp.Height; y++)
            {
                Color pixelColor = pictBmp.GetPixel(x, y);
                string[] RGB = new string[3];
                RGB[0] = Convert.ToString(pixelColor.R, 2).PadLeft(8, '0');
                RGB[1] = Convert.ToString(pixelColor.G, 2).PadLeft(8, '0');
                RGB[2] = Convert.ToString(pixelColor.B, 2).PadLeft(8, '0');

                foreach (string RGBColor in RGB)
                {
                    decodePix.Append(RGBColor[7]); 
                    if (decodePix.Length < 16) continue; // Write the value of the last bit from the shades until we create a string of size 16
                    if (decodePix.ToString() == "0000000000000000")
                    {
                        if (startSymbols) return; // If we already had initial characters, we consider the existing characters as the end and ending the method
                        else // Otherwise, we indicate that the initial characters were already finded and clear the string
                        {
                            startSymbols = true;
                            decodePix.Clear();
                        }
                    }
                    else if (!startSymbols) // If the characters are non-zero and we haven't received the initial zero characters yet, then we indicate that there is no encoded information
                    {
                        MessageBox.Show($"В даному зображенні немає закодованої інформації!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else // Append the value to the decode string and clear the string we write the bits from the RGB
                    {
                        decodeString += decodePix;
                        decodePix.Clear();
                    }
                }
            }
        }
    }

    private void buttonDecode_Click(object sender, EventArgs e) // Decode the image
    {
        string decodeString = ""; // Create variable decode string to get 16 bits code
        StringBuilder decodePix = new StringBuilder();
        pictBmp = new Bitmap(ActiveImage);
        int width = pictBmp.Width;
        int height = pictBmp.Height;
        ReadLastBits(width, height, ref decodeString, decodePix); // Fill the string

        if (decodeString == string.Empty) return;

        int deStr_Size = decodeString.Length / 16;
        byte[] bytes = new byte[deStr_Size];
        for (int i = 0; i < deStr_Size; i++) // loop to convert 16 bits of binary code to byte representation
        {
            var one_byte = decodeString.Substring(0, 16);
            var integerByte = Convert.ToInt32(one_byte, 2);
            bytes[i] = (byte)integerByte;
            decodeString = decodeString.Substring(16);
        }
        EncodingProvider provider = CodePagesEncodingProvider.Instance;
        Encoding.RegisterProvider(provider);
        decodeString = Encoding.GetEncoding(1251).GetString(bytes); // Convert from bytes to text characters
        MessageBox.Show($"{decodeString}", "Результат декодування", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void tb_codingText_TextChanged(object sender, EventArgs e) // Activation of the coding button is consistent with the visibility of the text in the text box
    {
        if (tb_codingText.Text == "") bt_coding.Enabled = false; 
        else bt_coding.Enabled = true;
    }
}
