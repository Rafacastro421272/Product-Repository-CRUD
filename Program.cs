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


Console.WriteLine("\n");

Product product2 = oService.GetProduct(2);

if (product2 != null)
{
    Console.WriteLine(product2);
}
else
{
    Console.WriteLine("No existe el producto");
}





Console.WriteLine("Presiona cualquier tecla para salir...");
Console.ReadKey();