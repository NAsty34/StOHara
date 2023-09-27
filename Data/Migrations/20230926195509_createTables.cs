using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class createTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
            
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[]
                {
                    "Id", "Name", "Surname", "Patronymic", "Email", "Phone", "RoleEntity",  "HashPassword", "Password", "IsActive", "IdUser", "CreatorDate"  
                    
                },
                values: new object[]
                {
                    new Guid("8250bcb8-7733-4aa0-99ec-2aaf106515fa"),
                    "Admin", "Admin", "Admin", "admin@gmail.com", "83295467721", "Admin", BCrypt.Net.BCrypt.HashPassword("12345"), "12345", true, null, DateTime.Now
                    
                }
            );
            migrationBuilder.InsertData(
                table: "AboutLending",
                columns: new[]
                {
                    "Id", "Header", "IdFile", "Description", "IsLeftPosition"

                },
                values: new object[]
                {
                    new Guid("1dc5c343-1aea-42dc-9447-b2b5bc6aa15f"),
                    "Уголок Ирландии в центре Костромы", "1b046eeb-209d-4814-971a-8077ca293d0a", "Традиционный паб с душевной живой музыкой и дружеской атмосферой. Работаем с 2018 года. Забронировать столик можно по телефону +7 (4942) 499-600", true
                }
            );
            migrationBuilder.InsertData(
                table: "AboutLending",
                columns: new[]
                {
                    "Id", "Header", "IdFile", "Description", "IsLeftPosition"

                },
                values: new object[]
                {
                    new Guid("8250bcb8-7733-4aa0-99ec-2aaf106515fa"),
                    "Кухня и бар", "750a5736-e6b7-4067-bf68-dfb32ac6a311", "Огромный выбор пива и виски, авторские коктейли от наших барменов. Сочное мясо, аппетитная рыба, традиционные ирландские блюда и закуски к пиву от шеф-повара Николая Семенцова. Готовим из костромских продуктов", false
                }
            );
           
            migrationBuilder.InsertData(
                table: "AboutLending",
                columns: new[]
                {
                    "Id", "Header", "IdFile", "Description", "IsLeftPosition"
                    },
                values: new object[]
                {
                    new Guid("9ff6466a-9926-4c8a-bf2a-e8269ce24464"),
                    "Живая музыка", "4bc2ca8b-5794-4b86-9569-d68830452bfe", "Каждую пятницу и в субботу отдыхаем под старый добрый русский рок. Расписание еженедельно публикуем в наших соцсетях", false
                }
            );
            migrationBuilder.InsertData(
                table: "AtmosphereLending",
                columns: new[]
                {
                    "Id", "Header", "IdFile", "Description", "IsLeftPosition"  
                    
                },
                values: new object[]
                {
                    new Guid("8250bcb8-7733-4aa0-99ec-2aaf106515fa"),
                    "Бизнес-ланч в St. O`Hara", "5047b820-02f7-4d49-9ef6-37e0b34ed71e", "По будням с 12 до 16 часов в пабе можно заказать комплексный обед. Только самые вкусные и сытные сочетания блюд. Чем обедаем сегодня, ищите в разделе «Меню»", false
                    
                }
            );
            migrationBuilder.InsertData(
                table: "AtmosphereLending",
                columns: new[]
                {
                    "Id", "Header", "IdFile", "Description", "IsLeftPosition"  
                    
                },
                values: new object[]
                {
                    new Guid("d61d0428-5d15-4179-b06f-8d840d067b3d"),
                    "Авторские коктейли", "467ac8c8-c97a-4e76-bcd1-779516bc0201", "Наши бармены разработали коктейли с невероятными сочетаниями ингредиентов, которые не оставят равнодушным даже искушенного гурмана. Найти их можно в разделе «Меню»", false
                    
                }
            );
            migrationBuilder.InsertData(
                table: "BannerLending",
                columns: new[]
                {
                    "Id", "Header", "IdFile"
                },
                values: new object[]
                {
                    new Guid("8250bcb8-7733-4aa0-99ec-2aaf106515fa"),
                    "ST.O'HARA ИРЛАНДСКИЙ ПАБ В КОСТРОМЕ", "2b34cc96-cf2e-427d-aa8f-e5b2b2531777"
                    
                }
            );
            migrationBuilder.InsertData(
                table: "SliderLending",
                columns: new[]
                {
                    "Id", "Header", "IdFile"
                },
                values: new object[]
                {
                    new Guid("8250bcb8-7733-4aa0-99ec-2aaf106515fa"),
                    "Атмосфера настоящего ирландского паба", "58dee16e-f5be-4916-9bff-b6817b2b7726"
                    
                }
            );
            migrationBuilder.InsertData(
                table: "SliderLending",
                columns: new[]
                {
                    "Id", "Header", "IdFile"
                },
                values: new object[]
                {
                    new Guid("78c3ab1c-56bf-49fe-bed8-2adbb04cf941"),
                    "Гриль-меню", "ea4db049-66a0-457a-98a8-cb50b804ac15"
                    
                }
            );
            migrationBuilder.InsertData(
                table: "SliderLending",
                columns: new[]
                {
                    "Id", "Header", "IdFile"
                },
                values: new object[]
                {
                    new Guid("e262dc8b-a158-4928-a656-5d71bf81759e"),
                    "Место встречи старых друзей", "419a472e-b4ff-4df2-8106-6faa4545455c"
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("72145235-5011-421a-9143-74c8b8976de1"),
                    "79399d7f-d94c-4d09-b03d-9f229dcb7f7d", 0, false, null, DateTime.Now
                }
            );
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("e9be297e-93db-43f9-88ab-a34ad5b31ff5"),
                    "7550980a-2f03-4b30-911c-b8bcdf8dcac0", 1, false, null, DateTime.Now
                }
            );
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("63723ba2-b6ad-4181-a452-24dd032f5dd6"),
                    "d6beac99-9503-45bb-b27f-ad259b6068ea", 2, false, null, DateTime.Now
                }
            );
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("49a6f0b2-ee98-4feb-aa23-f00744d8f51f"),
                    "e4c89db6-d48e-4ea3-ac52-4e66427d9c8a", 3, false, null, DateTime.Now
                }
            );
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("59233b49-266a-4b29-8f3d-0ee2277eef49"),
                    "f4cf4754-f0e7-4926-934f-0281fd018404", 4, false, null, DateTime.Now
                }
            );
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("31e9dc84-f574-4652-be15-0d562b5ac1b0"),
                    "5ea9ed99-0bdc-434e-a83e-7eec3a4c9388", 5, false, null, DateTime.Now
                }
            );
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("ff129896-fe6a-485d-8c21-6bc7852dd8f6"),
                    "a479b4d6-a059-4f3a-a90d-ef32d4ddd07e", 6, false, null, DateTime.Now
                }
            );
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("8fcc1630-5ad8-4987-9d0c-8b13041a7d00"),
                    "c08581e0-95e1-4c31-bb62-4419a56e6d0e", 7, false, null, DateTime.Now
                }
            );
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("6606ee75-811e-424c-82c8-d253eb7ef1a5"),
                    "dc60ebb1-2efd-4629-9343-381f0c17f6f2", 8, false, null, DateTime.Now
                }
            );
            
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("0b1b53de-9cab-4e5b-bc02-59e016f2586b"),
                    "15a2b9c0-0d44-4c0e-a92d-be8d4dd9bbc6", 9, false, null, DateTime.Now
                }
            );
            
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("52d83156-3a3a-49e1-b35e-ac78ed7df0aa"),
                    "40bd702c-2182-463d-9616-317192ff9a29", 10, false, null, DateTime.Now
                }
            );
            
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("5193c37e-5504-4a69-b3dd-a2ba7a879ff9"),
                    "40036030-ed86-4fe6-aee8-cd0dfd9a59ee", 11, false, null, DateTime.Now
                }
            );
            
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("ff9bdab8-bb16-484b-a478-a2f39c290688"),
                    "dbbe213f-c020-4ad4-b3e5-358cecf656c4", 12, false, null, DateTime.Now
                }
            );
            
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("d9f3d9c7-bb1b-4996-a357-402e3a90fa42"),
                    "0a7c44dc-7ea9-479f-a4ad-e09fdeac3b4b", 13, false, null, DateTime.Now
                }
            );
            
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("6e93ffbf-9e66-4a23-92d2-fee394dcab88"),
                    "16f4021f-592e-4d99-9110-597343ddcaea", 14, false, null, DateTime.Now
                }
            );

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("d4ed9e18-5e9d-4b30-89e6-b9e1744cff56"),
                    "45553d50-c417-4ef2-bd31-4428ff5f71b7", 1, true, null, DateTime.Now
                }
            );
            
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("7eeefdeb-3276-4334-9bee-c9febfbe6bd8"),
                    "a62de09a-05af-4b43-98c0-d9f6a504390f", 2, true, null, DateTime.Now
                }
            );
            
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("c38cb38b-7d20-4947-9d29-5722e9df4331"),
                    "5af9f867-11cc-4663-82ff-abb6849b78f5", 3, true, null, DateTime.Now
                }
            );
            
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("ed0e0f47-f8fb-4f86-a248-b6aa5c8449ec"),
                    "059f0c09-dbdd-4da4-89a3-3e84f1a4234e", 4, true, null, DateTime.Now
                }
            );
            
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[]
                {
                    "Id", "IdFile", "Position", "BusinessLunches", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("aa53504f-b391-4eeb-9695-b63815acce1b"),
                    "27ef9381-3a4b-40be-98d4-ebe56cda6c6d", 5, true, null, DateTime.Now
                }
            );
            
            
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("79399d7f-d94c-4d09-b03d-9f229dcb7f7d"),
                    "Coffee", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("7550980a-2f03-4b30-911c-b8bcdf8dcac0"),
                    "ColdSnack", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("d6beac99-9503-45bb-b27f-ad259b6068ea"),
                    "Deserts", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("e4c89db6-d48e-4ea3-ac52-4e66427d9c8a"),
                    "Djin", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("f4cf4754-f0e7-4926-934f-0281fd018404"),
                    "Menu2", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("5ea9ed99-0bdc-434e-a83e-7eec3a4c9388"),
                    "Tea", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("a479b4d6-a059-4f3a-a90d-ef32d4ddd07e"),
                    "visk2", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("c08581e0-95e1-4c31-bb62-4419a56e6d0e"),
                    "visk1", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("dc60ebb1-2efd-4629-9343-381f0c17f6f2"),
                    "visk3", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("1b046eeb-209d-4814-971a-8077ca293d0a"),
                    "About1", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("4bc2ca8b-5794-4b86-9569-d68830452bfe"),
                    "About2", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("750a5736-e6b7-4067-bf68-dfb32ac6a311"),
                    "About3", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("2b34cc96-cf2e-427d-aa8f-e5b2b2531777"),
                    "Banner", ".png", null, DateTime.Today
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("58dee16e-f5be-4916-9bff-b6817b2b7726"),
                    "Slider1", ".png", null, DateTime.Today
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("419a472e-b4ff-4df2-8106-6faa4545455c"),
                    "Slider2", ".png", null, DateTime.Today
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("ea4db049-66a0-457a-98a8-cb50b804ac15"), 
                    "Slider3", ".png", null, DateTime.Today
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("5047b820-02f7-4d49-9ef6-37e0b34ed71e"),
                    "Atmosphere1", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("467ac8c8-c97a-4e76-bcd1-779516bc0201"),
                    "Atmosphere2", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("45553d50-c417-4ef2-bd31-4428ff5f71b7"),
                    "lunch", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("a62de09a-05af-4b43-98c0-d9f6a504390f"),
                    "lunch", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("5af9f867-11cc-4663-82ff-abb6849b78f5"),
                    "lunch", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("059f0c09-dbdd-4da4-89a3-3e84f1a4234e"),
                    "lunch", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("27ef9381-3a4b-40be-98d4-ebe56cda6c6d"),
                    "lunch", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("15a2b9c0-0d44-4c0e-a92d-be8d4dd9bbc6"),
                    "menu1", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("40bd702c-2182-463d-9616-317192ff9a29"),
                    "menu2", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("40036030-ed86-4fe6-aee8-cd0dfd9a59ee"),
                    "menu3", ".png", null, DateTime.Today
                    
                }
            );
            
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("dbbe213f-c020-4ad4-b3e5-358cecf656c4"),
                    "menu4", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("0a7c44dc-7ea9-479f-a4ad-e09fdeac3b4b"),
                    "menu5", ".png", null, DateTime.Today
                    
                }
            );
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[]
                {
                    "Id", "Name", "Extension", "IdUser", "CreatorDate"
                },
                values: new object[]
                {
                    new Guid("16f4021f-592e-4d99-9110-597343ddcaea"),
                    "menu6", ".png", null, DateTime.Today
                    
                }
            );
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8250bcb8-7733-4aa0-99ec-2aaf106515fa")
            );
            migrationBuilder.DeleteData(
                table: "BannerLending",
                keyColumn: "Id",
                keyValue: new Guid("8250bcb8-7733-4aa0-99ec-2aaf106515fa")
            );
            migrationBuilder.DeleteData(
                table: "SliderLending",
                keyColumn: "Id",
                keyValue: new Guid("8250bcb8-7733-4aa0-99ec-2aaf106515fa")
            );
            migrationBuilder.DeleteData(
                table: "SliderLending",
                keyColumn: "Id",
                keyValue: new Guid("78c3ab1c-56bf-49fe-bed8-2adbb04cf941")
            );
            migrationBuilder.DeleteData(
                table: "SliderLending",
                keyColumn: "Id",
                keyValue: new Guid("e262dc8b-a158-4928-a656-5d71bf81759e")
            );
            migrationBuilder.DeleteData(
                table: "AtmosphereLending",
                keyColumn: "Id",
                keyValue: new Guid("8250bcb8-7733-4aa0-99ec-2aaf106515fa")
            );
            migrationBuilder.DeleteData(
                table: "AtmosphereLending",
                keyColumn: "Id",
                keyValue: new Guid("d61d0428-5d15-4179-b06f-8d840d067b3d")
            );
            migrationBuilder.DeleteData(
                table: "AboutLending",
                keyColumn: "Id",
                keyValue: new Guid("8250bcb8-7733-4aa0-99ec-2aaf106515fa")
            );
            migrationBuilder.DeleteData(
                table: "AboutLending",
                keyColumn: "Id",
                keyValue: new Guid("1dc5c343-1aea-42dc-9447-b2b5bc6aa15f")
            );
            migrationBuilder.DeleteData(
                table: "AboutLending",
                keyColumn: "Id",
                keyValue: new Guid("9ff6466a-9926-4c8a-bf2a-e8269ce24464")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("2b34cc96-cf2e-427d-aa8f-e5b2b2531777")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("4bc2ca8b-5794-4b86-9569-d68830452bfe")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("58dee16e-f5be-4916-9bff-b6817b2b7726")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("419a472e-b4ff-4df2-8106-6faa4545455c")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("467ac8c8-c97a-4e76-bcd1-779516bc0201")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("750a5736-e6b7-4067-bf68-dfb32ac6a311")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("5047b820-02f7-4d49-9ef6-37e0b34ed71e")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("ea4db049-66a0-457a-98a8-cb50b804ac15")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("1b046eeb-209d-4814-971a-8077ca293d0a")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("45553d50-c417-4ef2-bd31-4428ff5f71b7")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("a62de09a-05af-4b43-98c0-d9f6a504390f")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("5af9f867-11cc-4663-82ff-abb6849b78f5")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("059f0c09-dbdd-4da4-89a3-3e84f1a4234e")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("27ef9381-3a4b-40be-98d4-ebe56cda6c6d")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("15a2b9c0-0d44-4c0e-a92d-be8d4dd9bbc6")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("40bd702c-2182-463d-9616-317192ff9a29")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("40036030-ed86-4fe6-aee8-cd0dfd9a59ee")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("dbbe213f-c020-4ad4-b3e5-358cecf656c4")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("0a7c44dc-7ea9-479f-a4ad-e09fdeac3b4b")
            );
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("16f4021f-592e-4d99-9110-597343ddcaea")
            );
        }
    }
}
