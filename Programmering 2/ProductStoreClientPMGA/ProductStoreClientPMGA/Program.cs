using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProductStoreClient
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
    }

    class Program
    {
        private static List<Product> listOfProducts = new List<Product>();
        static void Main()
        {
            //Changes the color of the text in the console application.
            Console.ForegroundColor = ConsoleColor.Red;
            while (true)
            {
                Console.WriteLine("Hello and welcome to the Product Store.\n");
                Console.WriteLine("Press 'D' to Display all existing products.");
                Console.WriteLine("Press 'A' to Add a new product.");
                Console.WriteLine("Press 'E' to Erase a product.");
                Console.WriteLine("Press 'U' to Update an existing product.");
                Console.WriteLine("\nPress 'Escape' to quit.");

                ConsoleKey key = Console.ReadKey().Key;
                Console.Clear();
                switch (key)
                {
                    case ConsoleKey.Escape:
                        //Closes the console.
                        Environment.Exit(0);
                        break;

                    case ConsoleKey.A:
                        //This code runs when the key 'A' is clicked.
                        Product p = new Product();
                        Console.WriteLine("Enter the name of the product.");
                        //The product must have a name, this prevents it from not having a name.
                        while (true)
                        {
                            string tempName = Console.ReadLine();
                            if (tempName != "")
                            {
                                p.Name = tempName; 
                                break;
                            }
                            else
                            {
                                Console.WriteLine("The product must have a name.");
                                continue;
                            }
                        }
                        Console.Clear();

                        double tempPrice = 0;
                        Console.WriteLine("Enter the price of the product. (In $dollars)");
                        while (true)
                        {
                            tempPrice = 0;
                            //Only numbers and comma are allowed for the price, this code handles all the errors.
                            //If the user types in anything wrong, the user will have to try again untill the user succeeds.
                            if (double.TryParse(Console.ReadLine(), out tempPrice))
                            {
                                p.Price = tempPrice;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Only numbers and a maximum of one comma ( , ), no dots. For example(9,99).");
                                continue;
                            }

                        }
                        Console.Clear();

                        Console.WriteLine("Enter the category of the product.");
                        while (true)
                        {
                            string tempCat = Console.ReadLine();
                            if (tempCat != "")
                            {
                                p.Category = tempCat;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("The product must have a category.");
                                continue;
                            }
                        }
                        //Calls for the post method to post the new product to the productstore.
                        Post(p).Wait();
                        break;

                    case ConsoleKey.D:
                        //This code runs when the key 'D' is clicked.
                        Console.WriteLine("All existing products.\n");
                        //Displays all existing products.
                        AllProducts().Wait();
                        break;

                    case ConsoleKey.E:
                        //This code runs when the key 'E' is clicked.
                        Console.WriteLine("All existing products.\n");
                        //Displays all existing products.
                        AllProducts().Wait();
                        //Choose what item in the list to delete.
                        int keyInt = Console.ReadKey().KeyChar - 49;

                        if (keyInt > 0 && keyInt < listOfProducts.Count)
                        {
                            //Deletes the selected product from the productstore.
                            RunAsyncDeleteItem(listOfProducts[keyInt].Id).Wait();
                            //Deletes the selected product from the list.
                            listOfProducts.RemoveAt(keyInt);
                        }
                        Console.Clear();
                        Console.WriteLine("Remaining products.\n");
                        //Writes out all of the remaining products after the selected one has been deleted.
                        foreach (Product i in listOfProducts)
                        {
                            Console.WriteLine("{0}\t${1}\t{2}", UppercaseFirst(i.Name), i.Price, UppercaseFirst(i.Category));
                        }
                        break;

                    case ConsoleKey.U:
                        //This code runs when the key 'U' is clicked.
                        Console.WriteLine("Loading content...");
                        AllProducts().Wait();
                        Console.Clear();
                        Console.WriteLine("Select a product to update.\n");
                        //A number to be displayed before every product so it's easier for the user to see what product they choose to edit.
                        int listNumber = 1;
                        //Writes out all the existing products.
                        foreach (Product i in listOfProducts)
                        {
                            Console.WriteLine(listNumber++ + ": {0}\t${1}\t{2}", UppercaseFirst(i.Name), i.Price, UppercaseFirst(i.Category));
                        }

                        while (true)
                        {
                            //Saves the number in a int that the user pressed when choosing a product to update.
                            int selectToUpdate = Console.ReadKey().KeyChar - 48;
                            if (listOfProducts.Count + 1 >= selectToUpdate && selectToUpdate >= 1)
                            {
                                Console.Clear();
                                //-1 because the first product in the list is at spot 0 but I display it like it's on spot 1.
                                Product np = listOfProducts[selectToUpdate - 1];
                                //Changes the name.
                                Console.WriteLine("Enter a new name for the product, the current name is: " + UppercaseFirst(listOfProducts[selectToUpdate - 1].Name));
                                while (true)
                                {
                                    string tempNewName = Console.ReadLine();
                                    if (tempNewName != "")
                                    {
                                        np.Name = tempNewName;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("The product must have a name.");
                                        continue;
                                    }
                                }
                                Console.Clear();

                                Console.WriteLine("Enter a new price for the product (In $dollars), the current price is: $" + listOfProducts[selectToUpdate - 1].Price);
                                while (true)
                                {
                                    //Changes the price.
                                    tempPrice = 0;
                                    if (double.TryParse(Console.ReadLine(), out tempPrice))
                                    {
                                        np.Price = tempPrice;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Only numbers and a maximum of one comma ( , ), no dots. For example(9,99).");
                                        continue;
                                    }
                                }
                                Console.Clear();

                                //Changes the category.
                                Console.WriteLine("Enter a new category for the product, the current category is: " + UppercaseFirst(listOfProducts[selectToUpdate - 1].Category));
                                while (true)
                                {
                                    string tempNewCat = Console.ReadLine();
                                    if (tempNewCat != "")
                                    {
                                        np.Category = tempNewCat;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("The product must have a category.");
                                        continue;
                                    }
                                }

                                //Updates everything in the list about the choosen product.
                                listOfProducts[selectToUpdate - 1] = np;

                                //Updates everything in the product store about the choosen product.
                                UpdateProduct(listOfProducts[selectToUpdate - 1]).Wait();
                                break;
                            }
                        }
                    break;
                }
                Console.WriteLine("\nPress 'Enter' to go to the Menu or 'Escape' to quit.");
                ConsoleKeyInfo info = Console.ReadKey();
                //If 'Enter' is pressed the while loop starts over.
                if (info.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    continue;
                }
                //If 'Escape' is pressed the application will close.
                else if (info.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
        //Method for converting first letter to uppercase.
        static string UppercaseFirst(string s)
        {
            //Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            //Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        //Method for displaying the existing products in a numeric list.
        static async Task AllProducts()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:36861/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/products");
                if (response.IsSuccessStatusCode)
                {
                    List<Product> product = await response.Content.ReadAsAsync<List<Product>>();
                    listOfProducts = product;
                    int numberInList = 1;
                    foreach (Product i in product)
                    {
                        Console.WriteLine(numberInList++ + ": {0}\t${1}\t{2}", UppercaseFirst(i.Name), i.Price, UppercaseFirst(i.Category));
                    }
                }
            }
        }

        //Method for creating a product and sending it to the productstore.
        static async Task Post(Product p)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:36861/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("api/products", p);
                if (response.IsSuccessStatusCode)
                {
                    Uri newProductUrl = response.Headers.Location;
                }
            }
        }

        //Method for deleting a choosen product from the productstore.
        static async Task RunAsyncDeleteItem(int ID) 
        {
            using(var client = new HttpClient()) 
            {
                if(listOfProducts.Exists(x => x.Id == ID)) 
                {
                    client.BaseAddress = new Uri("http://localhost:36861/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.DeleteAsync("api/products/" + ID);
                }
            }
        }

        //Method for updating a choosen existing product in the productstore
        static async Task UpdateProduct(Product uProduct)
        {
            using (var client = new HttpClient())
            {
                if (listOfProducts.Exists(x => x.Id == uProduct.Id))
                {
                    client.BaseAddress = new Uri("http://localhost:36861/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.PutAsJsonAsync("api/products/", uProduct);
                }
            }
        }
    }
}