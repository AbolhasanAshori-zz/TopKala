using System.Net.Mime;
using System.Globalization;
using System.Threading.Tasks;
using System;
using TopKala.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TopKala.Utility.StaticData;
using System.Collections.Generic;
using TopKala.Models;

namespace TopKala.DataAccess.Initializer
{
    public static class DbInitializer
    {

        // TODO: Product Seeding Suspended! Proceed through admin panel
        #region Configutaions
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
            if ((await context.Database.GetPendingMigrationsAsync()).Count() > 0)
            {
                await context.Database.MigrateAsync();
            }

            await IdentityDbInitializer.SeedAsync(context);
            await SeedAsync(context);
        }

        private static async Task SeedAsync(ApplicationDbContext context)
        {
            await AddColorsAsync(context);
            await AddBrandsAsync(context);
            await AddCategoriesAsync(context);
            await AddSellersAsync(context);
            await AddInfosAsync(context);
            await AddProductsAsync(context);

            await context.SaveChangesAsync();
        }
        #endregion

        #region Data
        private static async Task AddColorsAsync(ApplicationDbContext context)
        {
            var colorsDbSet = context.Set<Color>();
            if (colorsDbSet.Any())
            {
                return;
            }

            var colors = new List<Color>();
            
            colors.Add(new Models.Color
            {
                Value = "خاکستری"
            });
            colors.Add(new Models.Color
            {
                Value = "نقره ای"
            });
            
            await colorsDbSet.AddRangeAsync(colors);
            await context.SaveChangesAsync();
        }

        private static async Task AddBrandsAsync(ApplicationDbContext context)
        {
            var brandsDbSet = context.Set<Brand>();
            if (brandsDbSet.Any())
            {
                return;
            }

            var brands = new List<Brand>();
            
            brands.Add(new Models.Brand
            {
                Value = "Apple"
            });
            
            await brandsDbSet.AddRangeAsync(brands);
            await context.SaveChangesAsync();
        }
        
        private static async Task AddCategoriesAsync(ApplicationDbContext context)
        {
            var categoriesDbSet = context.Set<Category>();
            if (categoriesDbSet.Any())
            {
                return;
            }

            var categories = new List<Category>();
            
            categories.Add(new Models.Category
            {
                Value = "گوشی موبایل",
                Parent = new Models.Category
                {
                    Value = "کالای دیجیتال"
                }
            });
            
            await categoriesDbSet.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }

        private static async Task AddSellersAsync(ApplicationDbContext context)
        {
            var sellersDbSet = context.Set<Seller>();
            if (sellersDbSet.Any())
            {
                return;
            }

            var sellers = new List<Seller>();
            
            sellers.Add(new Models.Seller
            {
                Value = "ناسا"
            });
            
            await sellersDbSet.AddRangeAsync(sellers);
            await context.SaveChangesAsync();
        }
        
        private static async Task AddInfosAsync(ApplicationDbContext context)
        {
            var infosDbSet = context.Set<Info>();
            if (infosDbSet.Any())
            {
                return;
            }

            var infos = new List<Info>();
            
            infos.Add(new Models.Info
            {
                Value = "حافظه داخلی"
            });

            infos.Add(new Models.Info
            {
                Value = "شبکه های ارتباطی"
            });

            infos.Add(new Models.Info
            {
                Value = "رزولوشن عکس"
            });

            infos.Add(new Models.Info
            {
                Value = "تعداد سیم کارت"
            });

            infos.Add(new Models.Info
            {
                Value = "ویژگی های خاص"
            });

            infos.Add(new Models.Info
            {
                Value = "ابعاد"
            });

            infos.Add(new Models.Info
            {
                Value = "توضیحات سیم کارت"
            });

            infos.Add(new Models.Info
            {
                Value = "وزن"
            });

            infos.Add(new Models.Info
            {
                Value = "تراشه"
            });

            infos.Add(new Models.Info
            {
                Value = "نوع پردازنده"
            });

            await infosDbSet.AddRangeAsync(infos);
            await context.SaveChangesAsync();
        }

        private static async Task AddProductsAsync(ApplicationDbContext context)
        {
            var productsDbSet = context.Set<Product>();
            if (productsDbSet.Any())
            {
                return;
            }

            var products = new List<Product>();

            var product = new Product();
            product.Title = "گوشی موبایل اپل مدل iPhone X ظرفیت 256 گیگابایت";
            product.EngTitle = "Apple iPhone X 256GB Mobile Phone";
            product.Description = @"<h2 class=""param-title"">نقد و بررسی تخصصی<span>گوشی موبایل اپل مدل iPhone X ظرفیت 256 گیگابایت</span></h2><div class=""parent-expert default""><div class=""content-expert""><p>اپل پس از شایعات فراوان از جدیدترین آیپد خود رونمایی کردتابه پرسش‌های فراوان علاقه‌مندان به محصولاتشپاسخ دهد. این آیپد با شباهت‌های فراوان به نسخه قبلیتولیدشده، اما از نظر برخی مشخصات فنی در ردهپایین‌تری قرار گرفته تا کاهش قیمتش توجیه منطقی داشتهباشد.اندازه و دقت نمایشگر آیپد جدید کاملا مشابه بانمایشگر نسخه قبلی این محصول است. اما در همین ابتدای کارلازممی‌دانیم که بگوییم این دو تبلت هیچ ارتباطیبا یکدیگر ندارند و از دو خانواده‌ی متفاوت و با قیمت‌هایمختلف هستند. صفحه‌نمایش آیپد جدید 9.7 اینچیدارای وضوح تصویر 1536 × 2048 پیکسل است و تراکم پیکسلی266پیکسل را ارائه می‌دهد. این تبلت به تراشه بهروزتر A9 و پردازنده‌ی کمکی M9 مجهز است. اما میزانحافظه‌ی رمآن از آیپد پروی 12.9 کمتر است. پردازنده‌یدو هسته‌ای این تبلت سرعتی برابر با 1.84گیگاهرتز دارد ودوگیگابایت حافظه‌ی رم این پردازنده را همراهیمی‌کند. در زمینه‌ی دوربین، آیپد جدید 9.7 اینچی نه‌تنهاپیشرفتی نداشته بلکه به سنسورهای ضعیف‌تری هم مجهزاست. اپل، یک حسگر 8 مگاپیکسلی برای دوربین اصلی تبلت 9.7اینچی به کار برده که می‌تواند با حداکثر کیفیتFullHD فیلم‌برداری کند. شاید بد نباشد بدانید که هیچ‌گونهفلشی این سنسور را همراهی نمی‌کند و عکاسی درمحیط‌های بسته توسط این سنسور چندان رضایت‌بخش نخواهد بود.دوربین دوم این تبلت را یک حسگر 1.2 مگاپیکسلیتشکیل می‌دهد که از ویژگی قابلیت تشخیص چهره (FaceDetection)بهره می‌برد. پرو 9.7 علاوه بر حافظه‌ی داخلی128 گیگابایتی، در نسخه‌ی 32 گیگابایتی هم عرضه شده است.همچنین این تبلت در دو نسخه‌ی LTE و WiFi تولید شدهکه مجموعا چهار انتخاب پیش روی کاربران قرار خواهد داشت.امامهم‌ترین تغییر ظاهری آیپد 9.7 اینچی مربوط بهحذف رنگ صورتی از رنگ‌های آن است. بنابراین، آیپد جدید 9.7اینچی در سه رنگ نقره‌ای، خاکستری و طلایی عرضهخواهد شد. درنهایت باید بگوییم که اپل تغییرات انقلابی ورادیکال محوری در جدیدترین تبلت خود ایجاد نکردهاست. اما بااین‌وجود باید گفت که آیپد 9.7 اینچیارزان‌ترینتبلت 10 اینچی تولیدشده‌ی تاریخ اپل تاکنون است.آیپد 9.7 بهترین تبلت برای افرادی است که می‌خواهند درازایپرداخت هزینه‌ی کمتر صاحب یک آیپد از شرکت مطرحاپل شوند.</p></div><div class=""sum-more""><span class=""show-more btn-link-border"">نمایش بیشتر</span><span class=""show-less btn-link-border"">بستن</span></div><div class=""shadow-box""></div></div><div class=""accordion default"" id=""accordionExample""><div class=""card""><div class=""card-header"" id=""headingOne""><h5 class=""mb-0""><button class=""btn btn-link"" type=""button"" data-toggle=""collapse"" data-target=""#collapseOne"" aria-expanded=""true"" aria-controls=""collapseOne"">Face ID (کسی به‌غیراز تو را نمی‌شناسم)</button></h5></div><div id=""collapseOne"" class=""collapse show"" aria-labelledby=""headingOne"" data-parent=""#accordionExample""><div class=""card-body""><img src=""~/img/single-product/1406986.jpg"" alt=""""><p>در فناوری تشخیص چهره‌ی اپل، یک دوربین وفرستنده‌ی مادون‌قرمز در بالای نمایشگر قرار داده‌شده‌ است؛ هنگامی‌که آیفونمی‌خواهد چهره‌ی شما را تشخیص دهد، فرستنده‌ی نورینامرئی را به ‌صورت شما می‌تاباند. دوربینمادون‌قرمز، این نور را تشخیصداده و با الگویی که قبلا از صورت شما ثبت کرده،مطابقت می‌دهد و در صورت یکی‌بودن، قفل گوشی راباز می‌کند. اپل ادعا کرده،الگوی صورت را با استفاده از سی هزار نقطه ذخیرهمی‌کند که دورزدن آن اصلا کار ساده‌ای نیست.استفاده از ماسک، عکس یا مواردمشابه نمی‌تواند امنیت اطلاعات شما را به خطراندازد؛ اما اگر برادر یا خواهر دوقلو دارید، بایدبرای امنیت اطلاعاتتان نگرانباشید.</p><img src=""~/img/single-product/1431842.jpg"" alt=""""><p>فقط یک نکته‌ی مثبت در مورد Face ID وجود ندارد؛بلکه موارد زیادی هستند که دانستن آن‌ها ضروری بهنظر می‌رسد. آیفون 10 فقطچهره‌ی یک نفر را می‌شناسد و نمی‌توانید مثلاثرانگشت، چند چهره را به آیفون معرفی کنید تا ازآن‌ها برای بازکردنش استفادهکنید. اگر آیفون در تلاش اول، صورت شما را نشناسد،باید نمایشگر را برای شناختن مجدد خاموش و روشنکنید یا اینکه آن را پایینببرید و دوباره روبه‌روی صورتتان قرار دهید. اینتمام ماجرا نیست؛ اگر آیفون 10 بین افراد زیادی کهچهره‌شان را نمی‌شناسددست‌به‌دست شود، دیگر به شناخت چهره عکس‌العملنشان نمی‌دهد و حتما باید از پین یا پسوورد برایبازکردن آن استفاده کنید تادوباره صورتتان را بشناسد.</p><img src=""~/img/single-product/1439653.jpg"" alt=""""><p>اپل سعی کرده نهایت استفاده را از این فناوری جدیدبکند؛ استفاده از آن برای ثبت تصاویر پرتره بادوربین سلفی، استفاده برایساختن شکلک‌های بامزه که آن‌ها را Animoji نامیدهاست و همچنین استفاده برای روشن نگه‌داشتن گوشیزمانی که کاربر به آن نگاهمی‌کند، مواردی هستند که iPhone X به کمک حسگراینفرارد، بدون نقص آن‌ها را انجام می‌دهد.</p></div></div></div><div class=""card""><div class=""card-header"" id=""headingTwo""><h5 class=""mb-0""><button class=""btn btn-link collapsed"" type=""button"" data-toggle=""collapse"" data-target=""#collapseTwo"" aria-expanded=""false"" aria-controls=""collapseTwo"">نمایش‌گر (رنگی‌تر از همیشه)</button></h5></div><div id=""collapseTwo"" class=""collapse"" aria-labelledby=""headingTwo"" data-parent=""#accordionExample""><div class=""card-body""><img src=""~/img/single-product/1429172.jpg"" alt=""""><p>اولین تجربه‌ی استفاده از پنل‌های اولد سامسونگروی گوشی‌های اپل، نتیجه‌ای جذاب برای همه بههمراه آورده است. مهندسیافزوده‌ی اپل روی این پنل‌ها باعث شده تا غلظترنگ‌ها کاملا متعادل باشد، نه مثل آیفون 8 کم باشدو نه مثل گلکسی S8 اشباعباشد تا از حد تعادل خارج شود. اپل این نمایشگر راSuper Retina نامیده تا ثابت کند، بهترین نمایشگرموجود در دنیا را طراحیکرده و از آن روی iPhone X استفاده کرده است.</p><img src=""~/img/single-product/1436228.jpg"" alt=""""><p>این نمایشگر در مقایسه با پنل‌های معمولی، مصرفانرژی کمتری دارد و این به خاطر استفاده ازپنل‌های اولد است؛ اما این مشخصهباعث نشده تا نور نمایشگر مثل محصولات دیگری کهپنل اولد دارند کم باشد؛ بلکه این پنل در هرشرایطی بهترین بازده‌ی ممکن رادارد. استفاده زیر نور شدید خورشید یا تاریکی مطلقفرقی ندارد، آیفون 10 خود را با شرایط تطبیقمی‌دهد. این تمام ماجرا نیست.در نمایشگر آیفون 10 نقطه‌ی حساس به تراز سفیدینور محیط قرار داده ‌شده‌اند تا آیفون 10 را باشرایط نوری محیطی که از آناستفاده می‌کنید، هماهنگ کند و تراز سفیدی نمایشگررا به‌صورت خودکار تغییر دهد. این فناوری که بانام True-Tone نام‌گذاریشده، کمک می‌کند رنگ‌های نمایشگر در هر نورینزدیک‌ترین غلظت و تراز سفیدی ممکن را به رنگ‌هایطبیعی داشته باشد.</p><img src=""~/img/single-product/1406339.jpg"" alt=""""></div></div></div><div class=""card""><div class=""card-header"" id=""headingThree""><h5 class=""mb-0""><button class=""btn btn-link collapsed"" type=""button"" data-toggle=""collapse"" data-target=""#collapseThree"" aria-expanded=""false"" aria-controls=""collapseThree"">طراحی و ساخت (قربانی کردن سنت برای امروزی شدن)</button></h5></div><div id=""collapseThree"" class=""collapse"" aria-labelledby=""headingThree"" data-parent=""#accordionExample""><div class=""card-body""><img src=""~/img/single-product/1398679.jpg"" alt=""""><p>اپل پا جای پای سامسونگ گذاشته و برای داشتن ظاهریامروزی و استفاده از جدیدترین فناوری‌های روز، سنتده‌ساله‌ی طراحیگوشی‌هایش را شکسته است. دیگر کلید خانه‌ای وجودندارد و تمام قاب جلویی را نمایشگر پر کرده است.حتی نمایشگر هم برایاستفاده از فناوری تشخیص چهره قربانی شده و قسمتبالایی آن بریده ‌شده است و لبه‌ی بالایی آن درمقایسه با هر گوشی دیگری کهتا به امروز دیده بودیم، حالت متفاوتی دارد. ابعادiPhone X کمی بزرگ‌تر از ابعاد آیفون 6 است؛ امانمایشگرش حدود یک اینچبزرگ‌تر از آیفون 6 است. این نشان می‌دهد، فاصله‌یلبه‌ها تا نمایشگر تا جای ممکن از طراحی جدیدترینآیفون اپل حذف‌ شده‌است.</p><img src=""~/img/single-product/1441226.jpg"" alt=""""><p>زبان طراحی جدید، آیفون 10 را به‌طور عجیبی به سمتتبدیل‌شدنش به یک کالای لوکس پیش برده است. نگاهکلی به طراحی این گوشینشان می‌دهد، اپل سنت‌شکنی کرده و کالایی تولیدکرده تا از هر نظر با نسخه‌های قبلی آیفون متفاوتباشد. استفاده از شیشه برایقاب پشتی، فلزی براق برای قاب اصلی، حذف کلید خانهو در انتها استفاده از نمایشگری بزرگ مواردی هستندکه نشان‌دهنده‌ی تفاوتiPhone X با نسخه‌های قبلی آیفون است. تمام سطوحآیفون براق و صیقلی طراحی ‌شده‌اند و تنها برآمدگیآیفون جدید مربوط بهمجموعه‌ی دوربین آن می‌شود که حدود یک میلی‌متریاز قاب پشتی بیرون زده است. برخلاف آیفون 8پلاس،دوربین‌های روی قاب پشتی بهحالت عمودی روی قاب پشتی قرار گرفته‌اند.</p><img src=""~/img/single-product/1418947.jpg"" alt=""""><p>آیفون جدید در دو رنگ خاکستری و نقره‌ای راهیبازار شده که در هر دو مدل قاب جلویی به رنگ مشکیاست و این بابت استفاده ازسنسورهای متعدد در بخش بالایی نمایشگر است. برخلافتمام آیفون‌های فلزی که تا امروز ساخته‌ شده‌اند،قاب اصلی از فلزی براقساخته ‌شده تا زیر نور خودنمایی کند.</p></div></div></div></div>";
            product.Brand = await context.Set<Brand>().FirstOrDefaultAsync(b => b.Value == "Apple");
            product.Category = await context.Set<Category>().FirstOrDefaultAsync(c => c.Value == "گوشی موبایل");
            product.Images = new List<ProductImage>()
            {
                new ProductImage() { Image = "1335154.jpg" },
                new ProductImage() { Image = "2114766.jpg" },
                new ProductImage() { Image = "3694075.jpg" },
                new ProductImage() { Image = "110197298.jpg" }
            };
            product.Colors = new List<Color>()
            {
                await context.Set<Color>().FirstOrDefaultAsync(b => b.Value == "خاکستری"),
                await context.Set<Color>().FirstOrDefaultAsync(b => b.Value == "نقره ای")
            };
            product.Comments = new List<Comment>()
            {
                new Comment() 
                {
                    Value = "لورم ایپسوم متن ساختگی"
                },
                new Comment() 
                {
                    Value = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است.",
                    Children = new List<Comment>
                    {
                        new Comment()
                        {
                            Value = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد.",
                            Children = new List<Comment>
                            {
                                new Comment()
                                {
                                    Value = "عالیه",
                                    Children = new List<Comment>
                                    {
                                        new Comment() { Value = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد.", }
                                    }
                                }
                            }
                        }
                    }
                },
                new Comment() {Value = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد."}
            };
            product.ProductTopInfo = new List<ProductInfo>()
            {
                new ProductInfo()
                {
                    Value = "256 گیگابایت",
                    Info = await context.Set<Info>().FirstOrDefaultAsync(i => i.Value == "حافظه داخلی")
                },
                new ProductInfo()
                {
                    Value = "2G,3G,4G",
                    Info = await context.Set<Info>().FirstOrDefaultAsync(i => i.Value == "شبکه های ارتباطی")
                },
                new ProductInfo()
                {
                    Value = "12.0 مگاپیکسل",
                    Info = await context.Set<Info>().FirstOrDefaultAsync(i => i.Value == "رزولوشن عکس")
                },
                new ProductInfo()
                {
                    Value = "تک",
                    Info = await context.Set<Info>().FirstOrDefaultAsync(i => i.Value == "تعداد سیم کارت")
                },
                new ProductInfo()
                {
                    Value = "مقاوم در برابر آب مناسب عکاسی مناسب عکاسی سلفی مناسب بازی مجهز به حس‌گر تشخیص چهره",
                    Info = await context.Set<Info>().FirstOrDefaultAsync(i => i.Value == "ویژگی های خاص")
                }
            };

            

            product.ProductInfoGroups = new List<ProductInfoGroup>()
            {
                new ProductInfoGroup()
                {
                    InfoGroup = new InfoGroup() { Value = "مشخصات کلی" },
                    ProductInfos = new List<ProductInfo>()
                    {
                        new ProductInfo()
                        {
                            Value = "7.7 × 70.9 × 143.6 میلی‌متر",
                            Info = await context.Set<Info>().FirstOrDefaultAsync(i => i.Value == "ابعاد")
                        },
                        new ProductInfo()
                        {
                            Value = "سایز نانو (8.8 × 12.3 میلی‌متر)",
                            Info = await context.Set<Info>().FirstOrDefaultAsync(i => i.Value == "توضیحات سیم کارت")
                        },
                        new ProductInfo()
                        {
                            Value = "174 گرم",
                            Info = await context.Set<Info>().FirstOrDefaultAsync(i => i.Value == "وزن")
                        },
                        product.ProductTopInfo.FirstOrDefault(i => i.Info.Value == "ویژگی های خاص"),
                        product.ProductTopInfo.FirstOrDefault(i => i.Info.Value == "تعداد سیم کارت")
                    } 
                },
                new ProductInfoGroup()
                {
                    InfoGroup = new InfoGroup() { Value = "پردازنده" },
                    ProductInfos = new List<ProductInfo>()
                    {
                        new ProductInfo()
                        {
                            Value = "Apple A11 Bionic Chipset",
                            Info = await context.Set<Info>().FirstOrDefaultAsync(i => i.Value == "تراشه")
                        },
                        new ProductInfo()
                        {
                            Value = "64 بیت",
                            Info = await context.Set<Info>().FirstOrDefaultAsync(i => i.Value == "نوع پردازنده")
                        } 
                    }
                }
            };
            product.ProductSellers = new List<ProductSeller>()
            {
                new ProductSeller()
                {
                    Price = 15390000,
                    Warrenty = "گارانتی اصالت و سلامت فیزیکی کالا",
                    Quantity = 5,
                    Color = await context.Set<Color>().FirstOrDefaultAsync(i => i.Value == "خاکستری"),
                    Seller = await context.Set<Seller>().FirstOrDefaultAsync(i => i.Value == "ناسا")
                }
            };
            product.ProductSellers.Append(
                new ProductSeller()
                {
                    Price = 15390000,
                    Warrenty = "گارانتی اصالت و سلامت فیزیکی کالا",
                    Quantity = 5,
                    Color = await context.Set<Color>().FirstOrDefaultAsync(i => i.Value == "نقره ای"),
                    Seller = await context.Set<Seller>().FirstOrDefaultAsync(i => i.Value == "ناسا")
                }
            );
            
            products.Add(product);
            
            await productsDbSet.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
        #endregion
    }
}