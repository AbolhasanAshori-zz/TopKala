using System.Threading.Tasks;
using System;
using TopKala.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TopKala.Utility.StaticData;

namespace TopKala.DataAccess.Initializer
{
    public static class DbInitializer
    {
        private static ApplicationDbContext _db;
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
            _db = context;
            if ((await _db.Database.GetPendingMigrationsAsync()).Count() > 0)
            {
                await _db.Database.MigrateAsync();
            }

            await SeedAsync();
        }

        public static async Task SeedAsync()
        {
            if (!_db.UserRole.Any())
            {
                await _db.UserRole.AddAsync(new Models.UserRole{ Name = SD_Role.Developer});
                await _db.UserRole.AddAsync(new Models.UserRole{ Name = SD_Role.Super_Admin});
                await _db.UserRole.AddAsync(new Models.UserRole{ Name = SD_Role.Admin});
                await _db.UserRole.AddAsync(new Models.UserRole{ Name = SD_Role.Moderator});
                await _db.UserRole.AddAsync(new Models.UserRole{ Name = SD_Role.Customer});
            }

            if (!_db.Users.Any())
            {
                await _db.Users.AddAsync(new Models.User
                {
                    Username = "Admin",
                    //* Password is Admin123*
                    PasswordHash = "$2a$11$R5/rmr1hr/5ySY6tw.VXteSyDrQ4zbbkBTM5StRL9WWqf2fz1pgE6",
                    Role = await _db.UserRole.FirstOrDefaultAsync(r => r.Name == SD_Role.Admin),
                    Email = "admin@gmail.com",
                    FirstName = "Mr.",
                    LastName = "Admin",
                    PhoneNumber = "09123456789",
                    IdNumber = "1234567812345678",
                    CardNumber = "12345678",
                    IsActive = true,
                    IsEmailConfirmed = true,
                });
            }

            await _db.SaveChangesAsync();
        }
    }
}

// namespace TopKala.DataAccess.Data
// {
//     public class DbInitializer
//     {
//         #region Data Initialization
//         private static IEnumerable<Brand> BrandsInitialization()
//         {
//             var brands = new List<Brand>();

//             var brand1 = new Brand()
//             {
//                 Id = 1,
//                 Name = "َApple"
//             };

//             brands.AddRange(new List<Brand> { brand1 });

//             return brands;
//         }
//         private static IEnumerable<Category> CategoriesInitialization()
//         {
//             var categories = new List<Category>();

//             var category1 = new Category()
//             {
//                 Id = 1,
//                 Name = "کالای دیجیتال"
//             };

//             var category2 = new Category()
//             {
//                 Id = 2,
//                 Name = "موبایل",
//                 ParentId = 1,
//                 // Parent = category1
//             };
            
//             categories.AddRange(new List<Category> { category1, category2 });

//             return categories;
//         }
//         private static IEnumerable<Product> ProductsInitialization()
//         {
//             var products = new List<Product>();

//             var product1 = new Product()
//             {
//                 Id = 1,
//                 Title = "گوشی موبایل اپل مدل iPhone X ظرفیت 256 گیگابایت",
//                 EngTitle = "Apple iPhone X 256GB Mobile Phone",
//                 Description = @"<h2 class=""param-title"">نقد و بررسی تخصصی<span>گوشی موبایل اپل مدل iPhone X ظرفیت 256 گیگابایت</span></h2><div class=""parent-expert default""><div class=""content-expert""><p>اپل پس از شایعات فراوان از جدیدترین آیپد خود رونمایی کردتابه پرسش‌های فراوان علاقه‌مندان به محصولاتشپاسخ دهد. این آیپد با شباهت‌های فراوان به نسخه قبلیتولیدشده، اما از نظر برخی مشخصات فنی در ردهپایین‌تری قرار گرفته تا کاهش قیمتش توجیه منطقی داشتهباشد.اندازه و دقت نمایشگر آیپد جدید کاملا مشابه بانمایشگر نسخه قبلی این محصول است. اما در همین ابتدای کارلازممی‌دانیم که بگوییم این دو تبلت هیچ ارتباطیبا یکدیگر ندارند و از دو خانواده‌ی متفاوت و با قیمت‌هایمختلف هستند. صفحه‌نمایش آیپد جدید 9.7 اینچیدارای وضوح تصویر 1536 × 2048 پیکسل است و تراکم پیکسلی266پیکسل را ارائه می‌دهد. این تبلت به تراشه بهروزتر A9 و پردازنده‌ی کمکی M9 مجهز است. اما میزانحافظه‌ی رمآن از آیپد پروی 12.9 کمتر است. پردازنده‌یدو هسته‌ای این تبلت سرعتی برابر با 1.84گیگاهرتز دارد ودوگیگابایت حافظه‌ی رم این پردازنده را همراهیمی‌کند. در زمینه‌ی دوربین، آیپد جدید 9.7 اینچی نه‌تنهاپیشرفتی نداشته بلکه به سنسورهای ضعیف‌تری هم مجهزاست. اپل، یک حسگر 8 مگاپیکسلی برای دوربین اصلی تبلت 9.7اینچی به کار برده که می‌تواند با حداکثر کیفیتFullHD فیلم‌برداری کند. شاید بد نباشد بدانید که هیچ‌گونهفلشی این سنسور را همراهی نمی‌کند و عکاسی درمحیط‌های بسته توسط این سنسور چندان رضایت‌بخش نخواهد بود.دوربین دوم این تبلت را یک حسگر 1.2 مگاپیکسلیتشکیل می‌دهد که از ویژگی قابلیت تشخیص چهره (FaceDetection)بهره می‌برد. پرو 9.7 علاوه بر حافظه‌ی داخلی128 گیگابایتی، در نسخه‌ی 32 گیگابایتی هم عرضه شده است.همچنین این تبلت در دو نسخه‌ی LTE و WiFi تولید شدهکه مجموعا چهار انتخاب پیش روی کاربران قرار خواهد داشت.امامهم‌ترین تغییر ظاهری آیپد 9.7 اینچی مربوط بهحذف رنگ صورتی از رنگ‌های آن است. بنابراین، آیپد جدید 9.7اینچی در سه رنگ نقره‌ای، خاکستری و طلایی عرضهخواهد شد. درنهایت باید بگوییم که اپل تغییرات انقلابی ورادیکال محوری در جدیدترین تبلت خود ایجاد نکردهاست. اما بااین‌وجود باید گفت که آیپد 9.7 اینچیارزان‌ترینتبلت 10 اینچی تولیدشده‌ی تاریخ اپل تاکنون است.آیپد 9.7 بهترین تبلت برای افرادی است که می‌خواهند درازایپرداخت هزینه‌ی کمتر صاحب یک آیپد از شرکت مطرحاپل شوند.</p></div><div class=""sum-more""><span class=""show-more btn-link-border"">نمایش بیشتر</span><span class=""show-less btn-link-border"">بستن</span></div><div class=""shadow-box""></div></div><div class=""accordion default"" id=""accordionExample""><div class=""card""><div class=""card-header"" id=""headingOne""><h5 class=""mb-0""><button class=""btn btn-link"" type=""button"" data-toggle=""collapse"" data-target=""#collapseOne"" aria-expanded=""true"" aria-controls=""collapseOne"">Face ID (کسی به‌غیراز تو را نمی‌شناسم)</button></h5></div><div id=""collapseOne"" class=""collapse show"" aria-labelledby=""headingOne"" data-parent=""#accordionExample""><div class=""card-body""><img src=""~/img/single-product/1406986.jpg"" alt=""""><p>در فناوری تشخیص چهره‌ی اپل، یک دوربین وفرستنده‌ی مادون‌قرمز در بالای نمایشگر قرار داده‌شده‌ است؛ هنگامی‌که آیفونمی‌خواهد چهره‌ی شما را تشخیص دهد، فرستنده‌ی نورینامرئی را به ‌صورت شما می‌تاباند. دوربینمادون‌قرمز، این نور را تشخیصداده و با الگویی که قبلا از صورت شما ثبت کرده،مطابقت می‌دهد و در صورت یکی‌بودن، قفل گوشی راباز می‌کند. اپل ادعا کرده،الگوی صورت را با استفاده از سی هزار نقطه ذخیرهمی‌کند که دورزدن آن اصلا کار ساده‌ای نیست.استفاده از ماسک، عکس یا مواردمشابه نمی‌تواند امنیت اطلاعات شما را به خطراندازد؛ اما اگر برادر یا خواهر دوقلو دارید، بایدبرای امنیت اطلاعاتتان نگرانباشید.</p><img src=""~/img/single-product/1431842.jpg"" alt=""""><p>فقط یک نکته‌ی مثبت در مورد Face ID وجود ندارد؛بلکه موارد زیادی هستند که دانستن آن‌ها ضروری بهنظر می‌رسد. آیفون 10 فقطچهره‌ی یک نفر را می‌شناسد و نمی‌توانید مثلاثرانگشت، چند چهره را به آیفون معرفی کنید تا ازآن‌ها برای بازکردنش استفادهکنید. اگر آیفون در تلاش اول، صورت شما را نشناسد،باید نمایشگر را برای شناختن مجدد خاموش و روشنکنید یا اینکه آن را پایینببرید و دوباره روبه‌روی صورتتان قرار دهید. اینتمام ماجرا نیست؛ اگر آیفون 10 بین افراد زیادی کهچهره‌شان را نمی‌شناسددست‌به‌دست شود، دیگر به شناخت چهره عکس‌العملنشان نمی‌دهد و حتما باید از پین یا پسوورد برایبازکردن آن استفاده کنید تادوباره صورتتان را بشناسد.</p><img src=""~/img/single-product/1439653.jpg"" alt=""""><p>اپل سعی کرده نهایت استفاده را از این فناوری جدیدبکند؛ استفاده از آن برای ثبت تصاویر پرتره بادوربین سلفی، استفاده برایساختن شکلک‌های بامزه که آن‌ها را Animoji نامیدهاست و همچنین استفاده برای روشن نگه‌داشتن گوشیزمانی که کاربر به آن نگاهمی‌کند، مواردی هستند که iPhone X به کمک حسگراینفرارد، بدون نقص آن‌ها را انجام می‌دهد.</p></div></div></div><div class=""card""><div class=""card-header"" id=""headingTwo""><h5 class=""mb-0""><button class=""btn btn-link collapsed"" type=""button"" data-toggle=""collapse"" data-target=""#collapseTwo"" aria-expanded=""false"" aria-controls=""collapseTwo"">نمایش‌گر (رنگی‌تر از همیشه)</button></h5></div><div id=""collapseTwo"" class=""collapse"" aria-labelledby=""headingTwo"" data-parent=""#accordionExample""><div class=""card-body""><img src=""~/img/single-product/1429172.jpg"" alt=""""><p>اولین تجربه‌ی استفاده از پنل‌های اولد سامسونگروی گوشی‌های اپل، نتیجه‌ای جذاب برای همه بههمراه آورده است. مهندسیافزوده‌ی اپل روی این پنل‌ها باعث شده تا غلظترنگ‌ها کاملا متعادل باشد، نه مثل آیفون 8 کم باشدو نه مثل گلکسی S8 اشباعباشد تا از حد تعادل خارج شود. اپل این نمایشگر راSuper Retina نامیده تا ثابت کند، بهترین نمایشگرموجود در دنیا را طراحیکرده و از آن روی iPhone X استفاده کرده است.</p><img src=""~/img/single-product/1436228.jpg"" alt=""""><p>این نمایشگر در مقایسه با پنل‌های معمولی، مصرفانرژی کمتری دارد و این به خاطر استفاده ازپنل‌های اولد است؛ اما این مشخصهباعث نشده تا نور نمایشگر مثل محصولات دیگری کهپنل اولد دارند کم باشد؛ بلکه این پنل در هرشرایطی بهترین بازده‌ی ممکن رادارد. استفاده زیر نور شدید خورشید یا تاریکی مطلقفرقی ندارد، آیفون 10 خود را با شرایط تطبیقمی‌دهد. این تمام ماجرا نیست.در نمایشگر آیفون 10 نقطه‌ی حساس به تراز سفیدینور محیط قرار داده ‌شده‌اند تا آیفون 10 را باشرایط نوری محیطی که از آناستفاده می‌کنید، هماهنگ کند و تراز سفیدی نمایشگررا به‌صورت خودکار تغییر دهد. این فناوری که بانام True-Tone نام‌گذاریشده، کمک می‌کند رنگ‌های نمایشگر در هر نورینزدیک‌ترین غلظت و تراز سفیدی ممکن را به رنگ‌هایطبیعی داشته باشد.</p><img src=""~/img/single-product/1406339.jpg"" alt=""""></div></div></div><div class=""card""><div class=""card-header"" id=""headingThree""><h5 class=""mb-0""><button class=""btn btn-link collapsed"" type=""button"" data-toggle=""collapse"" data-target=""#collapseThree"" aria-expanded=""false"" aria-controls=""collapseThree"">طراحی و ساخت (قربانی کردن سنت برای امروزی شدن)</button></h5></div><div id=""collapseThree"" class=""collapse"" aria-labelledby=""headingThree"" data-parent=""#accordionExample""><div class=""card-body""><img src=""~/img/single-product/1398679.jpg"" alt=""""><p>اپل پا جای پای سامسونگ گذاشته و برای داشتن ظاهریامروزی و استفاده از جدیدترین فناوری‌های روز، سنتده‌ساله‌ی طراحیگوشی‌هایش را شکسته است. دیگر کلید خانه‌ای وجودندارد و تمام قاب جلویی را نمایشگر پر کرده است.حتی نمایشگر هم برایاستفاده از فناوری تشخیص چهره قربانی شده و قسمتبالایی آن بریده ‌شده است و لبه‌ی بالایی آن درمقایسه با هر گوشی دیگری کهتا به امروز دیده بودیم، حالت متفاوتی دارد. ابعادiPhone X کمی بزرگ‌تر از ابعاد آیفون 6 است؛ امانمایشگرش حدود یک اینچبزرگ‌تر از آیفون 6 است. این نشان می‌دهد، فاصله‌یلبه‌ها تا نمایشگر تا جای ممکن از طراحی جدیدترینآیفون اپل حذف‌ شده‌است.</p><img src=""~/img/single-product/1441226.jpg"" alt=""""><p>زبان طراحی جدید، آیفون 10 را به‌طور عجیبی به سمتتبدیل‌شدنش به یک کالای لوکس پیش برده است. نگاهکلی به طراحی این گوشینشان می‌دهد، اپل سنت‌شکنی کرده و کالایی تولیدکرده تا از هر نظر با نسخه‌های قبلی آیفون متفاوتباشد. استفاده از شیشه برایقاب پشتی، فلزی براق برای قاب اصلی، حذف کلید خانهو در انتها استفاده از نمایشگری بزرگ مواردی هستندکه نشان‌دهنده‌ی تفاوتiPhone X با نسخه‌های قبلی آیفون است. تمام سطوحآیفون براق و صیقلی طراحی ‌شده‌اند و تنها برآمدگیآیفون جدید مربوط بهمجموعه‌ی دوربین آن می‌شود که حدود یک میلی‌متریاز قاب پشتی بیرون زده است. برخلاف آیفون 8پلاس،دوربین‌های روی قاب پشتی بهحالت عمودی روی قاب پشتی قرار گرفته‌اند.</p><img src=""~/img/single-product/1418947.jpg"" alt=""""><p>آیفون جدید در دو رنگ خاکستری و نقره‌ای راهیبازار شده که در هر دو مدل قاب جلویی به رنگ مشکیاست و این بابت استفاده ازسنسورهای متعدد در بخش بالایی نمایشگر است. برخلافتمام آیفون‌های فلزی که تا امروز ساخته‌ شده‌اند،قاب اصلی از فلزی براقساخته ‌شده تا زیر نور خودنمایی کند.</p></div></div></div></div>",
//                 BrandId = 1,
//                 CategoryId = 2,
//             };

            
//             products.AddRange(new List<Product> { product1 });

//             return products;
//         }
//         private static IEnumerable<Seller> SellersInitialization()
//         {
//             var sellers = new List<Seller>();

//             var seller1 = new Seller()
//             {
//                 Id = 1,
//                 Name = "ناسا"
//             };

//             sellers.AddRange(new List<Seller> { seller1 });

//             return sellers;
//         }
//         private static IEnumerable<ProductColor> ProductColorsInitialization()
//         {
//             var productColors = new List<ProductColor>();

//             var pc1 = new ProductColor()
//             {
//                 Id = 1,
//                 Name = "خاکستری",
//                 ProductId = 1
//             };

//             var pc2 = new ProductColor()
//             {
//                 Id = 2,
//                 Name = "نقره ای",
//                 ProductId = 1
//             };

//             productColors.AddRange(new List<ProductColor> { pc1, pc2 });

//             return productColors;
//         }
//         private static IEnumerable<ProductSeller> ProductSellersInitialization()
//         {
//             var productSellers = new List<ProductSeller>();

//             var ps1 = new ProductSeller()
//             {
//                 Id = 1,
//                 Price = 15390000,
//                 Warrenty = "گارانتی اصالت و سلامت فیزیکی کالا",
//                 Quantity = 10,
//                 ProductId = 1,
//                 SellerId = 1,
//                 ProductColorId = 1
//             };

//             var ps2 = new ProductSeller()
//             {
//                 Id = 2,
//                 Price = 15390000,
//                 Warrenty = "گارانتی اصالت و سلامت فیزیکی کالا",
//                 Quantity = 10,
//                 ProductId = 1,
//                 SellerId = 1,
//                 ProductColorId = 2
//             };

//             productSellers.AddRange(new List<ProductSeller> { ps1, ps2 });

//             return productSellers;
//         }
//         private static IEnumerable<Info> InfosInitialization()
//         {
//             var infos = new List<Info>();

//             var info1 = new Info()
//             {
//                 Id = 1,
//                 Name = "حافظه داخلی"
//             };

//             var info2 = new Info()
//             {
//                 Id = 2,
//                 Name = "شبکه های ارتباطی"
//             };

//             var info3 = new Info()
//             {
//                 Id = 3,
//                 Name = "رزولوشن عکس"
//             };

//             var info4 = new Info()
//             {
//                 Id = 4,
//                 Name = "تعداد سیم کارت"
//             };

//             var info5 = new Info()
//             {
//                 Id = 5,
//                 Name = "ویژگی های خاص"
//             };

//             infos.AddRange(new List<Info> { info1, info2, info3, info4, info5 });

//             return infos;
//         }
//         private static IEnumerable<ProductInfo> ProductInfosInitialization()
//         {
//             var productInfos = new List<ProductInfo>();

//             var pi1 = new ProductInfo()
//             {
//                 Id = 1,
//                 Value = "256 گیگابایت",
//                 InfoId = 1,
//                 ProductId = 1
//             };

//             var pi2 = new ProductInfo()
//             {
//                 Id = 2,
//                 Value = "2G,3G,4G",
//                 InfoId = 2,
//                 ProductId = 1
//             };

//             var pi3 = new ProductInfo()
//             {
//                 Id = 3,
//                 Value = "12.0 مگاپیکسل",
//                 InfoId = 3,
//                 ProductId = 1
//             };

//             var pi4 = new ProductInfo()
//             {
//                 Id = 4,
//                 Value = "تک",
//                 InfoId = 4,
//                 ProductId = 1
//             };

//             var pi5 = new ProductInfo()
//             {
//                 Id = 5,
//                 Value = "مقاوم در برابر آب مناسب عکاسی مناسب عکاسی سلفی مناسب بازی مجهز به حس‌گر تشخیص چهره",
//                 InfoId = 5,
//                 ProductId = 1
//             };

//             productInfos.AddRange(new List<ProductInfo> { pi1, pi2, pi3, pi4, pi5 });

//             return productInfos;
//         }
//         private static IEnumerable<ProductImage> ProductImagesInitialization()
//         {
//             var productImages = new List<ProductImage>();

//             var pi1 = new ProductImage()
//             {
//                 Id = 1,
//                 Image = "1335154.jpg",
//                 ProductId = 1
//             };

//             var pi2 = new ProductImage()
//             {
//                 Id = 2,
//                 Image = "2114766.jpg",
//                 ProductId = 1
//             };

//             var pi3 = new ProductImage()
//             {
//                 Id = 3,
//                 Image = "3694075.jpg",
//                 ProductId = 1
//             };

//             var pi4 = new ProductImage()
//             {
//                 Id = 4,
//                 Image = "110197298.jpg",
//                 ProductId = 1
//             };

//             productImages.AddRange(new List<ProductImage> { pi1 });

//             return productImages;
//         }
//         private static IEnumerable<Comment> CommentsInitialization()
//         {
//             var comments = new List<Comment>();

//             var comment1 = new Comment()
//             {
//                 Id = 1,
//                 Content = "لورم ایپسوم متن ساختگی",
//                 ProductId = 1
//             };

//             var comment2 = new Comment()
//             {
//                 Id = 2,
//                 Content = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است.",
//                 ProductId = 1
//             };

//             var comment3 = new Comment()
//             {
//                 Id = 3,
//                 Content = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد.",
//                 ParentId = 2,
//                 ProductId = 1
//             };

//             var comment4 = new Comment()
//             {
//                 Id = 4,
//                 Content = "عالیه",
//                 ParentId = 3,
//                 ProductId = 1
//             };

//             var comment5 = new Comment()
//             {
//                 Id = 5,
//                 Content = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد.",
//                 ParentId = 4,
//                 ProductId = 1
//             };

//             var comment6 = new Comment()
//             {
//                 Id = 6,
//                 Content = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد.",
//                 ProductId = 1
//             };

//             comments.AddRange(new List<Comment> { comment1, comment2, comment3, comment4, comment5, comment6 });

//             return comments;
//         }
//         #endregion

//         #region Configs
//         public static void Initialize(DbContext context)
//         {
//             context.Database.EnsureDeleted();
//             context.Database.Migrate();
//         }
//         public static void Seed(DbContext context)
//         {
//             Initialize(context);

//             if (!context.Set<Brand>().Any())
//             {
//                 context.Set<Brand>().AddRange(BrandsInitialization());
//             }

//             if (!context.Set<Category>().Any())
//             {
//                 context.Set<Category>().AddRange(CategoriesInitialization());
//             }

//             if (!context.Set<Product>().Any())
//             {
//                 context.Set<Product>().AddRange(ProductsInitialization());
//             }

//             if (!context.Set<Seller>().Any())
//             {
//                 context.Set<Seller>().AddRange(SellersInitialization());
//             }

//             if (!context.Set<ProductColor>().Any())
//             {
//                 context.Set<ProductColor>().AddRange(ProductColorsInitialization());
//             }

//             if (!context.Set<ProductSeller>().Any())
//             {
//                 context.Set<ProductSeller>().AddRange(ProductSellersInitialization());
//             }

//             if (!context.Set<Info>().Any())
//             {
//                 context.Set<Info>().AddRange(InfosInitialization());
//             }

//             if (!context.Set<ProductInfo>().Any())
//             {
//                 context.Set<ProductInfo>().AddRange(ProductInfosInitialization());
//             }

//             if (!context.Set<ProductImage>().Any())
//             {
//                 context.Set<ProductImage>().AddRange(ProductImagesInitialization());
//             }

//             if (!context.Set<Comment>().Any())
//             {
//                 context.Set<Comment>().AddRange(CommentsInitialization());
//             }

//             context.SaveChanges();
//         }
//         #endregion
//     }
// }