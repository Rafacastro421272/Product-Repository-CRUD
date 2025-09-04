using Actividad_1.Domain;
using Actividad_1.Service;

ProductService oService = new ProductService();

List<Product> lp = oService.GetProducts();

if (lp.Count > 0)
{
    foreach (Product p in lp)
    {
        Console.WriteLine(p);
    }

}
else
{
    Console.WriteLine("No products found.");
}
