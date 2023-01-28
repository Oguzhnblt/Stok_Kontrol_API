using Stok_Kontrol_API.Entities.Entities;

namespace Stok_Kontrol_API.WebUI.Areas.UserArea.Models
{
    public class ProductCart
    {
        private Dictionary<int, ProductCartVM> _cart = null;


        public ProductCart()
        {
            _cart = new Dictionary<int, ProductCartVM>();
        }

        public List<ProductCartVM> CartProductList
        {
            get
            {
                return _cart.Values.ToList(); // Sepet koleksiyonundaki değerleri (ürünleri) döndür.
            }
        }



        // SEPETE EKLE 
        public void AddCart(ProductCartVM product)
        {
            if (!_cart.ContainsKey(product.ID))
            {
                _cart.Add(product.ID, product); // Eğer ürün sepette bulunmuyorsa, ürünün ID'sini koleksiyona key olarak, kendisini de value olarak eklesin.
            }
            else
            {
                _cart[product.ID].Quantity++; // Eğer ürün sepette bulunmuyorsa, ürünün ID'sini koleksiyona key olarak, kendisini de value olarak eklesin.
            }
        }


        // SEPETTEN SİL
        public void RemoveCart(int productID)
        {
            _cart.Remove(productID); // Sepetten ilgili ID'ye sahip ürünü sil.
        }


        // SEPET MİKTARI AZALT
        public void DecreaseCart(int productID)
        {
            _cart[productID].Quantity--; // İlgili ürünün miktarını bir azalt.

            if (_cart[productID].Quantity <= 0) // Eğer ürünün miktarı sepette 0 veya daha az ise, aşağıdaki koşulu gerçekleştir.
            {
                _cart.Remove(productID); // Ürünü sepetten sil.
            }
        }


        // SEPET MİKTARI ARTTIR
        public void IncreaseCart(int productID)
        {
            _cart[productID].Quantity++; // İlgili ürünün miktarını sepette bir arttır.

            if (_cart[productID].Quantity >= _cart[productID].Stock) // Eğer sepette,ürünün miktarı ürünün stoğundan fazla olmaya çalışıyorsa
            {
                _cart[productID].Quantity = (short)_cart[productID].Stock; // Ürünün miktarı en fazla stok kadar olabilir.
            }
        }
    }
}
