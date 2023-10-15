using Microsoft.EntityFrameworkCore;
using TraderApi.Data;
namespace TraderApi.Models
{
    public class Purchaser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile1 { get; set; }
        public string? Mobile2 { get; set; }
        public string? Mobile3 { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate  { get; set; }
        public bool IsDeleted { get; set; }

    }

    }


//public static class PurchaserEndpoints
//{
//	public static void MapPurchaserEndpoints (this IEndpointRouteBuilder routes)
//    {
//        routes.MapGet("/api/Purchaser", async (TraderApiContext db) =>
//        {
//            return await db.Purchaser.ToListAsync();
//        })
//        .WithName("GetAllPurchasers");

//        routes.MapGet("/api/Purchaser/{id}", async (int Id, TraderApiContext db) =>
//        {
//            return await db.Purchaser.FindAsync(Id)
//                is Purchaser model
//                    ? Results.Ok(model)
//                    : Results.NotFound();
//        })
//        .WithName("GetPurchaserById");

//        routes.MapPut("/api/Purchaser/{id}", async (int Id, Purchaser purchaser, TraderApiContext db) =>
//        {
//            var foundModel = await db.Purchaser.FindAsync(Id);

//            if (foundModel is null)
//            {
//                return Results.NotFound();
//            }

//            db.Update(purchaser);

//            await db.SaveChangesAsync();

//            return Results.NoContent();
//        })
//        .WithName("UpdatePurchaser");

//        routes.MapPost("/api/Purchaser/", async (Purchaser purchaser, TraderApiContext db) =>
//        {
//            db.Purchaser.Add(purchaser);
//            await db.SaveChangesAsync();
//            return Results.Created($"/Purchasers/{purchaser.Id}", purchaser);
//        })
//        .WithName("CreatePurchaser");


//        routes.MapDelete("/api/Purchaser/{id}", async (int Id, TraderApiContext db) =>
//        {
//            if (await db.Purchaser.FindAsync(Id) is Purchaser purchaser)
//            {
//                db.Purchaser.Remove(purchaser);
//                await db.SaveChangesAsync();
//                return Results.Ok(purchaser);
//            }

//            return Results.NotFound();
//        })
//        .WithName("DeletePurchaser");
//    }
//}}
