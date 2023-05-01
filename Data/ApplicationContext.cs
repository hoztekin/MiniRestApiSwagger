using BookDemo.Models;

namespace BookDemo.Data
{
	public static class ApplicationContext
	{
		public static List<Book> Books { get; set; }
		static ApplicationContext()
		{
			Books = new List<Book>()
			{
			new Book(){ Id=1, Title="Ortadoğu", Price=150},
			new Book(){ Id=2, Title="Olasılıksız", Price=300},
			new Book(){ Id=3, Title="Şu Çılgın Türkler", Price=250},
			};
		}
	}
}
