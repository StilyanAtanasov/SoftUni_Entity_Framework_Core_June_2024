using Articles;
using MongoDB.Driver;


// --- 01. Create the Database ---
MongoClient client = new("mongodb://localhost:27017/");
IMongoDatabase db = client.GetDatabase("Articles");

// --- 02. Read ---
IMongoCollection<Article> articlesCollection = db.GetCollection<Article>("Articles");
List<Article> articles = await (await articlesCollection.FindAsync(_ => true)).ToListAsync();

articles.ForEach(a => Console.WriteLine(a.Name));

// --- 03. Create a new article ---
Article article = new Article
{
    Author = "Steve Jobs",
    Date = "05-05-2005",
    Name = "The story of Apple",
    Rating = "60"
};

await articlesCollection.InsertOneAsync(article);

// --- 04. Update ---
articles = await (await articlesCollection.FindAsync(_ => true)).ToListAsync();
foreach (Article ar in articles)
{
    if (!int.TryParse(ar.Rating, out int newRating)) continue;
    newRating += 10;

    FilterDefinition<Article> filterDefinition = Builders<Article>.Filter.Eq(a => a.Id, ar.Id);
    UpdateDefinition<Article> update = Builders<Article>.Update.Set(a => a.Rating, newRating.ToString());

    await articlesCollection.UpdateOneAsync(filterDefinition, update);

}

// --- 05. Delete ---
FilterDefinition<Article> deleteFilter = Builders<Article>.Filter.Eq(a => a.Rating, "60");
await articlesCollection.DeleteManyAsync(deleteFilter);