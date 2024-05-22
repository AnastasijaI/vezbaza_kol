using vezba1.Models;

namespace vezba1.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) 
            { 
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();


                //Kniga
                if(!context.Knigi.Any())
                {
                    context.Knigi.AddRange(new List<Kniga>()
                    {
                        new Kniga()
                        {
                            Naslov="Dekameron",
                            Godina=2001,
                            BrStrani=350,
                            Opis="Kniga koja ke ve posoci kako da razmisluvate i kako da se spravuvate so nekoi raboti",
                            Zanr="Self-Help",
                            Tirazh=3,
                            Izdavac="Italy",
                            SlikaUrl="https://th.bing.com/th/id/R.0c2c2f5c58fc9acbebdc2869bf33718a?rik=hA%2fnK68LXjoy0Q&riu=http%3a%2f%2fjazzybee-verlag.de%2fwp-content%2fuploads%2f2016%2f12%2fboccaccio-dekameron-front.jpg&ehk=5Qi%2fcwl3RXCb6dYLv46JrAlgnx5TiaR9NQcuJHWwXww%3d&risl=&pid=ImgRaw&r=0"
                        },
                        new Kniga()
                        {
                            Naslov="Veronica decide morir",
                            Godina=1999,
                            BrStrani=350,
                            Opis="Kniga za edna mlada nesrekjna devojka",
                            Zanr="Psychology Novel",
                            Tirazh=3,
                            Izdavac="Brazil",
                            SlikaUrl="https://www.funeralnatural.net/sites/default/files/libro/imagen/veronika_dec.jpg"
                        }
                    });
                    context.SaveChanges();
                }
                //Avtor
                if (!context.Avtori.Any())
                {
                    context.Avtori.AddRange(new List<Avtor>()
                    {
                        new Avtor()
                        {
                            Ime="Govanni",
                            Prezime="Boccaccio",
                            Pol="Male",
                            Nacionalnost="Italian",
                            DatumRagjanje=DateTime.ParseExact("11.05.1350", "dd.MM.yyyy", null)
                        },
                        new Avtor()
                        {
                            Ime="Paulo",
                            Prezime="Coelho",
                            Pol="Male",
                            Nacionalnost="Brazilian",
                            DatumRagjanje=DateTime.ParseExact("11.05.1947", "dd.MM.yyyy", null)
                        }
                    });
                    context.SaveChanges();
                }
                //AvtorKniga
                if (!context.AvtorKnigi.Any())
                {
                    context.AvtorKnigi.AddRange(new List<AvtorKniga>()
                    {
                        new AvtorKniga()
                        {
                            KnigaId=1,
                            AvtorId=1,
                        },
                        new AvtorKniga()
                        {
                            KnigaId=2,
                            AvtorId=2,
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
