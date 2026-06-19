using Microsoft.Data.SqlClient;

string connectionString =
    "Server=tcp:new-server-vlad.database.windows.net,1433;" +
    "Initial Catalog=mydatabase;" +
    "User ID=Aadmin;" +
    "Password=Liu-Kang;" +
    "Encrypt=True;" +
    "TrustServerCertificate=False;";

try
{
    using var connection = new SqlConnection(connectionString);

    await connection.OpenAsync();


  
        string dropOrders = @"DROP TABLE IF EXISTS Orders";
        string dropProducts = @"DROP TABLE IF EXISTS Products";

        using (SqlCommand cmd = new SqlCommand(dropOrders, connection))
        {
            cmd.ExecuteNonQuery();
        }

        using (SqlCommand cmd = new SqlCommand(dropProducts, connection))
        {
            cmd.ExecuteNonQuery();
        }

        Console.WriteLine("Tables dropped\n");
    

    Console.WriteLine("Успішно підключено!");

    string createTablesSql = @"




CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL DEFAULT 0
);

CREATE TABLE Orders (
    Id INT PRIMARY KEY IDENTITY,
    ProductId INT NOT NULL
        FOREIGN KEY REFERENCES Products(Id),
    Quantity INT NOT NULL,
    OrderDate DATETIME DEFAULT GETDATE()
);";

    using (var createCmd = new SqlCommand(createTablesSql, connection))
    {
        await createCmd.ExecuteNonQueryAsync();
    }

    Console.WriteLine("Таблиці створено");

    string insertSql = @"
INSERT INTO Products (Name, Price, Stock)
VALUES
('Клавіатура', 850, 20),
('Миша', 350, 35),
('Монітор', 7200, 8);

INSERT INTO Orders (ProductId, Quantity)
VALUES
(1, 2),
(2, 5),
(3, 1);";

    using (var insertCmd = new SqlCommand(insertSql, connection))
    {
        await insertCmd.ExecuteNonQueryAsync();
    }

    Console.WriteLine("Дані додано");

    Console.WriteLine();
    Console.WriteLine("- ЗАПИТ 1 -");
    Console.WriteLine("Товари дорожче 500 грн:");

    string query1 = @"
SELECT Name, Price, Stock
FROM Products
WHERE Price > 500
ORDER BY Price DESC";

    using (var cmd1 = new SqlCommand(query1, connection))
    using (var reader1 = await cmd1.ExecuteReaderAsync())
    {
        while (await reader1.ReadAsync())
        {
            Console.WriteLine(
                $"{reader1["Name"]} | " +
                $"{reader1["Price"]} грн | " +
                $"Залишок: {reader1["Stock"]}");
        }
    }

    Console.WriteLine();
    Console.WriteLine("- ЗАПИТ 2 -");
    Console.WriteLine("Замовлення:");

    string query2 = @"
SELECT p.Name,
       o.Quantity,
       o.OrderDate
FROM Orders o
JOIN Products p
ON p.Id = o.ProductId
ORDER BY o.OrderDate DESC";

    using (var cmd2 = new SqlCommand(query2, connection))
    using (var reader2 = await cmd2.ExecuteReaderAsync())
    {
        while (await reader2.ReadAsync())
        {
            Console.WriteLine(
                $"{reader2["Name"]} | " +
                $"{reader2["Quantity"]} шт | " +
                $"{Convert.ToDateTime(reader2["OrderDate"]):dd.MM.yyyy}");
        }
    }

    Console.WriteLine();
    Console.WriteLine("- ЗАПИТ 3 -");
    Console.WriteLine("Загальна вартість товарів на складі:");

    string query3 = @"
SELECT
    Name,
    Price,
    Stock,
    Price * Stock AS TotalValue
FROM Products
ORDER BY TotalValue DESC";

    using (var cmd3 = new SqlCommand(query3, connection))
    using (var reader3 = await cmd3.ExecuteReaderAsync())
    {
        while (await reader3.ReadAsync())
        {
            Console.WriteLine(
                $"{reader3["Name"]} | " +
                $"Ціна: {reader3["Price"]} | " +
                $"Кількість: {reader3["Stock"]} | " +
                $"Сума: {reader3["TotalValue"]}");
        }
    }

    Console.WriteLine();
    Console.WriteLine(" Усі запити виконано успішно!");
}
catch (Exception ex)
{
    Console.WriteLine($" Помилка: {ex.Message}");
}
