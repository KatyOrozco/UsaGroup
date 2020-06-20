namespace TraigoApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Airlines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        PesoVolumetrico = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Commands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TotalComanda = c.Double(),
                        TotalComTraigo = c.Double(),
                        TotalComViajero = c.Double(),
                        CommandStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CommandStatus", t => t.CommandStatusId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CommandStatusId);
            
            CreateTable(
                "dbo.CommandStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Email = c.String(),
                        Cedula = c.String(),
                        CelularEc = c.String(),
                        CelularUs = c.String(),
                        CityId = c.Int(nullable: false),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuotationId = c.Int(nullable: false),
                        CommandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Commands", t => t.CommandId, cascadeDelete: true)
                .ForeignKey("dbo.Quotations", t => t.QuotationId, cascadeDelete: true)
                .Index(t => t.QuotationId)
                .Index(t => t.CommandId);
            
            CreateTable(
                "dbo.Quotations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        NombreItem = c.String(),
                        Precio = c.Double(nullable: false),
                        Peso = c.Double(nullable: false),
                        ComisionTraigo = c.Double(nullable: false),
                        ComisionTraigoIva = c.Double(nullable: false),
                        ComisionViajero = c.Double(nullable: false),
                        ISD = c.Double(nullable: false),
                        VentaExitosa = c.Boolean(nullable: false),
                        FechaCotizacion = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        FechaVentaExitosa = c.DateTime(nullable: false),
                        Largo = c.Double(nullable: false),
                        Ancho = c.Double(nullable: false),
                        Alto = c.Double(nullable: false),
                        PesoVolumetrico = c.Double(nullable: false),
                        Volumen = c.Double(nullable: false),
                        TackingNumber = c.String(),
                        Courrier = c.String(),
                        UserId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        PaymentId = c.Int(nullable: false),
                        QuotationStatusId = c.Int(nullable: false),
                        FechaVuelta = c.DateTime(),
                        TripId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Payments", t => t.PaymentId, cascadeDelete: false)
                .ForeignKey("dbo.QuotationStatus", t => t.QuotationStatusId, cascadeDelete: true)
                .ForeignKey("dbo.Trips", t => t.TripId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.CategoryId)
                .Index(t => t.PaymentId)
                .Index(t => t.QuotationStatusId)
                .Index(t => t.TripId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuotationStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Vuelta = c.DateTime(nullable: false),
                        Ida = c.DateTime(nullable: false),
                        FechaHasta = c.DateTime(nullable: false),
                        FechaDesde = c.DateTime(nullable: false),
                        NotasImportantes = c.String(),
                        UserId = c.Int(nullable: false),
                        UsAddressId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        CodigoReserva = c.String(),
                        AirlineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Airlines", t => t.AirlineId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.UsAddresses", t => t.UsAddressId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.UsAddressId)
                .Index(t => t.StatusId)
                .Index(t => t.AirlineId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UsAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Direccion = c.String(),
                        Ciudad = c.String(),
                        Zipcode = c.String(),
                        Nombre = c.String(),
                        Telefono = c.String(),
                        UserId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Abbreviation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TransactionControls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TravelerUserId = c.Int(nullable: false),
                        QuotationId = c.Int(nullable: false),
                        TripId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quotations", t => t.QuotationId, cascadeDelete: true)
                .ForeignKey("dbo.Trips", t => t.TripId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.QuotationId)
                .Index(t => t.TripId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TransactionControls", "UserId", "dbo.Users");
            DropForeignKey("dbo.TransactionControls", "TripId", "dbo.Trips");
            DropForeignKey("dbo.TransactionControls", "QuotationId", "dbo.Quotations");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Items", "QuotationId", "dbo.Quotations");
            DropForeignKey("dbo.Quotations", "UserId", "dbo.Users");
            DropForeignKey("dbo.Quotations", "TripId", "dbo.Trips");
            DropForeignKey("dbo.Trips", "UserId", "dbo.Users");
            DropForeignKey("dbo.Trips", "UsAddressId", "dbo.UsAddresses");
            DropForeignKey("dbo.UsAddresses", "UserId", "dbo.Users");
            DropForeignKey("dbo.UsAddresses", "StateId", "dbo.States");
            DropForeignKey("dbo.Trips", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Trips", "AirlineId", "dbo.Airlines");
            DropForeignKey("dbo.Quotations", "QuotationStatusId", "dbo.QuotationStatus");
            DropForeignKey("dbo.Quotations", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.Quotations", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Items", "CommandId", "dbo.Commands");
            DropForeignKey("dbo.Commands", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Commands", "CommandStatusId", "dbo.CommandStatus");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TransactionControls", new[] { "TripId" });
            DropIndex("dbo.TransactionControls", new[] { "QuotationId" });
            DropIndex("dbo.TransactionControls", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.UsAddresses", new[] { "StateId" });
            DropIndex("dbo.UsAddresses", new[] { "UserId" });
            DropIndex("dbo.Trips", new[] { "AirlineId" });
            DropIndex("dbo.Trips", new[] { "StatusId" });
            DropIndex("dbo.Trips", new[] { "UsAddressId" });
            DropIndex("dbo.Trips", new[] { "UserId" });
            DropIndex("dbo.Quotations", new[] { "TripId" });
            DropIndex("dbo.Quotations", new[] { "QuotationStatusId" });
            DropIndex("dbo.Quotations", new[] { "PaymentId" });
            DropIndex("dbo.Quotations", new[] { "CategoryId" });
            DropIndex("dbo.Quotations", new[] { "UserId" });
            DropIndex("dbo.Items", new[] { "CommandId" });
            DropIndex("dbo.Items", new[] { "QuotationId" });
            DropIndex("dbo.Users", new[] { "CityId" });
            DropIndex("dbo.Commands", new[] { "CommandStatusId" });
            DropIndex("dbo.Commands", new[] { "UserId" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TransactionControls");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.States");
            DropTable("dbo.UsAddresses");
            DropTable("dbo.Status");
            DropTable("dbo.Trips");
            DropTable("dbo.QuotationStatus");
            DropTable("dbo.Payments");
            DropTable("dbo.Quotations");
            DropTable("dbo.Items");
            DropTable("dbo.Users");
            DropTable("dbo.CommandStatus");
            DropTable("dbo.Commands");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
            DropTable("dbo.Categories");
            DropTable("dbo.Airlines");
        }
    }
}
