using System.Windows;

namespace WpfApp
{
    public partial class ProductWindow : Window
    {
        // Сюда мы передадим товар, если редактируем его
        public Product? EditableProduct { get; set; }

        public ProductWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Введите название!");
                return;
            }

            if (!decimal.TryParse(PriceBox.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Цена должна быть числом больше 0!");
                return;
            }

            if (!int.TryParse(CountBox.Text, out int count) || count < 0)
            {
                MessageBox.Show("Количество должно быть целым числом!");
                return;
            }

            // Если товара нет, создаем новый, иначе обновляем старый
            if (EditableProduct == null)
            {
                EditableProduct = new Product();
            }

            EditableProduct.Name = NameBox.Text;
            EditableProduct.Price = price;
            EditableProduct.Description = DescBox.Text;
            EditableProduct.Count = count;
            EditableProduct.ImagePath = ImageBox.Text;

            DialogResult = true; // Говорим главному окну, что всё ок
            Close();
        }
    }
}