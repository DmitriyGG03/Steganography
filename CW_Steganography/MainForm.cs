using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stenography;
public partial class MainForm : Form
{
    Image ActiveImage; // Обране зображення, з яким працюємо
    Bitmap pictBmp; // карта пикселей изображения (необходима для корректировки последних битов пикселей)

    public MainForm()
    {
        InitializeComponent();
    }

    private void bt_choosePict_Click(object sender, EventArgs e) // Обираємо зображення та виводимо на екран
    {
        using (OpenFileDialog dialog = new OpenFileDialog())
        {
            dialog.Filter = "Зображення:|*.jpg;*.jpeg;*.bmp;*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream filestrm = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    ActiveImage = Image.FromStream(filestrm);
                    pb_activePhoto.Image = ActiveImage; // вывод на экран

                    bt_encoding.Enabled = true; // Активуємо кнопки декодування та видалення кодування
                    bt_delCoding.Enabled = true;
                    tb_codingText.Enabled = true; // Активуємо текстове поле

                }
            }

        }
    }

    private void bt_coding_Click(object sender, EventArgs e) // кодирование в выбранное изображение
    {
        string bitString = GetStringOfBinaryCode(); // 00000000 + usertext(every number 16 symbols) + 00000000
        pictBmp = new Bitmap(ActiveImage);
        bool stopCircle = false;

        //Перебираємо всі пікселі рисунку поки не змінимо останні біти кожного кольору на значення з bitString
        for (int x = 0; x < pictBmp.Width; x++)
        {
            for (int y = 0; y < pictBmp.Height; y++)
            {
                Color pixelColor = pictBmp.GetPixel(x, y); //Завантажуємо піксель
                // метод изменяющий последний бит на нужный
                string[] RGB = new string[3]; // Створюжмо масив для значень трьох відтінків кольорів 
                //Записуємо у масив значення пікселю додаючи нулі на початку до 8 довжини
                RGB[0] = Convert.ToString(pixelColor.R, 2).PadLeft(8, '0');
                RGB[1] = Convert.ToString(pixelColor.G, 2).PadLeft(8, '0');
                RGB[2] = Convert.ToString(pixelColor.B, 2).PadLeft(8, '0');

                for (int i = 0; i < 3; i++)
                {
                    if (bitString.Length == 0)
                    {
                        stopCircle = true;
                        break;
                    }
                    else ChangeColor(ref bitString, ref RGB[i]);
                }

                pictBmp.SetPixel(x, y, Color.FromArgb(Convert.ToInt32(RGB[0], 2), Convert.ToInt32(RGB[1], 2), Convert.ToInt32(RGB[2], 2)));
            }
            if (stopCircle) break;
        }

        DialogResult dr = MessageBox.Show($"Чи бажаєте зберегти результат?", "Текст успішно закодовано", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (dr == DialogResult.Yes)
        {
            //Зберігаємо цю картинку
            using (SaveFileDialog dialog = new SaveFileDialog()) // выбор куда сохранять
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

    private string GetStringOfBinaryCode() // получение строки двоичного кода для последующего кодирования
    {
        EncodingProvider provider = CodePagesEncodingProvider.Instance;
        Encoding.RegisterProvider(provider);
        // Отримуємо числові еквіваленти символів в тексті для кодування
        byte[] bytes = Encoding.GetEncoding(1251).GetBytes(tb_codingText.Text);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 16; i++) sb.Append('0'); // При читанні будуть означати початок і кінець
        for (int i = 0; i < bytes.Length; i++) sb.Append(Convert.ToString(bytes[i], 2).PadLeft(16, '0'));
        for (int i = 0; i < 16; i++) sb.Append('0');

        return sb.ToString();
    }


    void ChangeColor(ref string bitString, ref string color)
    {
        // Видаляємо останній біт з кольору в двійковому форматі
        color = color.Remove(color.Length - 1); //00000000 - ..0

        // Записуємо значення біту з рядку, де в двійковому вигляді представлений текст
        color += bitString[0];
        bitString = bitString.Remove(0, 1);
    }

    private void buttonDecode_Click(object sender, EventArgs e) // декодирование изображения
    {
        bool startReading = false;
        string decodeString = "";
        StringBuilder decodePix = new StringBuilder();
        bool is_brake = false;
        pictBmp = new Bitmap(ActiveImage);
        for (int x = 0; x < pictBmp.Width; x++)
        {
            for (int y = 0; y < pictBmp.Height; y++)
            {
                Color pixelColor = pictBmp.GetPixel(x, y);
                string[] RGB = new string[3];
                RGB[0] = Convert.ToString(pixelColor.R, 2).PadLeft(8, '0');
                RGB[1] = Convert.ToString(pixelColor.G, 2).PadLeft(8, '0');
                RGB[2] = Convert.ToString(pixelColor.B, 2).PadLeft(8, '0');

                /*-----------------------------------------------------------------------------------------------*/

                if (decodePix.Length != 16) // тут мы добавляем последний бит цвет в декодируемую строку символа decodePix - это один символ
                {
                    decodePix.Append(RGB[0][7]);
                }
                else if (decodePix.ToString() == "0000000000000000") // если это конец то заканчиваем
                {
                    if (startReading == false)
                    {
                        startReading = true;
                        decodePix.Clear();
                        decodePix.Append(RGB[0][7]);
                    }
                    else
                    {
                        is_brake = true;
                        break;
                    }
                }
                else // если строка заполнилась то записываем в конечную строку (decodeString) и очищаем строку для одного символа (decodePix)
                {
                    if (startReading)
                    {
                        decodeString += decodePix;
                        decodePix.Clear();
                        decodePix.Append(RGB[0][7]);
                    }
                }

                /*-----------------------------------------------------------------------------------------------*/

                if (decodePix.Length != 16) // тут мы добавляем последний бит цвет в декодируемую строку символа decodePix - это один символ
                {
                    decodePix.Append(RGB[1][7]);
                }
                else if (decodePix.ToString() == "0000000000000000") // если это конец то заканчиваем
                {
                    if (startReading == false)
                    {
                        startReading = true;
                        decodePix.Clear();
                        decodePix.Append(RGB[1][7]);
                    }
                    else
                    {
                        is_brake = true;
                        break;
                    }
                }
                else // если строка заполнилась то записываем в конечную строку (decodeString) и очищаем строку для одного символа (decodePix)
                {
                    if (startReading)
                    {
                        decodeString += decodePix;
                        decodePix.Clear();
                        decodePix.Append(RGB[1][7]);
                    }
                }

                /*-----------------------------------------------------------------------------------------------*/

                if (decodePix.Length != 16) // тут мы добавляем последний бит цвет в декодируемую строку символа decodePix - это один символ
                {
                    decodePix.Append(RGB[2][7]);
                }
                else if (decodePix.ToString() == "0000000000000000") // если это конец то заканчиваем
                {
                    if (startReading == false)
                    {
                        startReading = true;
                        decodePix.Clear();
                        decodePix.Append(RGB[2][7]);
                    }
                    else
                    {
                        is_brake = true;
                        break;
                    }
                }
                else // если строка заполнилась то записываем в конечную строку (decodeString) и очищаем строку для одного символа (decodePix)
                {
                    if (startReading)
                    {
                        decodeString += decodePix;
                        decodePix.Clear();
                        decodePix.Append(RGB[2][7]);
                    }
                }
            }
            if (is_brake)
                break;
        }

        int deStr_Size = decodeString.Length / 16;
        byte[] bytes = new byte[deStr_Size];
        for (int i = 0; i < deStr_Size; i++) // цикл для перевода 16 битов двоичного кода в байтовое представление
        {
            var one_byte = decodeString.Substring(0, 16);
            var integerByte = Convert.ToInt32(one_byte, 2);
            bytes[i] = (byte)integerByte;
            decodeString = decodeString.Substring(16);
        }
        EncodingProvider provider = CodePagesEncodingProvider.Instance;
        Encoding.RegisterProvider(provider);
        decodeString = Encoding.GetEncoding(1251).GetString(bytes); // перевод из байтов в символы текста
        MessageBox.Show($"{decodeString}", "Результат декодування", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void tb_codingText_TextChanged(object sender, EventArgs e)
    {
        if (tb_codingText.Text == "") bt_coding.Enabled = false;
        else bt_coding.Enabled = true;
    }
}
