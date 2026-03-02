# CetStudentBook

---

## Ödev: Books CRUD

Bu projede `Book` modeli ve CRUD sayfalarını yaptım. Scaffolding kullanmadım, controller ve view'ları yazdım.

### eklenenler

- Book modeli (Name, Author, PublishDate, PageCount, IsSecondHand, validation)
- DbContext'e `DbSet<Book>` 
- BooksController  (Index, Create, Edit, Delete)
- Her işlem için ayrı view 
- Navbar'a Books linki 
- Veritabanı olarak SQLite 

### Projeyi çalıştırmak için


Veritabanını oluşturun ve projeyi başlatın:
```
dotnet ef database update
dotnet run
```


### Ekran görüntüleri


<img width="2784" height="1521" alt="Ekran görüntüsü 2026-03-02 200733" src="https://github.com/user-attachments/assets/33fb3df5-9801-4bd0-a9ba-aefce6bdecf9" />
<img width="2781" height="1531" alt="Ekran görüntüsü 2026-03-02 200705" src="https://github.com/user-attachments/assets/3d523220-81fd-4954-bbd5-1c1efb58ed4f" />
<img width="2787" height="1524" alt="Ekran görüntüsü 2026-03-02 200717" src="https://github.com/user-attachments/assets/f4380527-b5e0-46a0-8a38-d0874dbd9653" />






