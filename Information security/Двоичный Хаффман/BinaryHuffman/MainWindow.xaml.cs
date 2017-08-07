using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Collections;

namespace BinaryHuffman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttoninput_Click(object sender, RoutedEventArgs e)
        {
            string input = textboxinput.Text;
            HuffmanTree huffmanTree = new HuffmanTree();
            huffmanTree.Build(input);

            //Коды
            listboxcodes.Items.Clear();
            List<string> EncodedSymb = huffmanTree.EncodedSymbols(input);

            for (int i = 0; i < EncodedSymb.Count; i++)
                listboxcodes.Items.Add(EncodedSymb[i]);
             
            // Кодируем
            BitArray encoded = huffmanTree.Encode(input);
            string buf = "";
            foreach (bool bit in encoded)
            {
                buf += (bit ? 1 : 0) + "";
            }
            textboxencoded.Text = buf;

            //Декодируем
            string decoded = huffmanTree.Decode(encoded);
            textboxdecoded.Text = decoded;

        }
    }
}
