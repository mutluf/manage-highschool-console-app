# ManageHighSchool

![alt-text](https://github.com/mutluf/Deneme/blob/main/homework.gif.gif)


### Projede 2 Class'ı Neden Yaptım? InMemoryDatas / IdProvider

#### InMemoryDatas
Öncelikle yapmış olduğumuz proje bir konsol uygulaması olduğu için statik çalışmak durumunda kaldık. Verileri tutacağımız listeyi ihtiyaç duyduğumuz her yerde oluşturmak veri kaybı ve veri tutarsızlıklarına sebep olacaktı. Bunu gidermek amacıyla gerekli veriler için birer list sağlayacak sınıf oluşturdum. Aşağıda sadece Student sınıfı için oluşturduğum bir örneği görmekteyiz. Burada ihtiyacımız olan List ögesi bellekte mevcutsa var olan listeyi kullanacağız eğer mevcut değilse yeni bir liste kullanarak yeni listeyi kullanacağız.   

```c#
public class StudentList
    {
        public static List<Student> studentListsInstance;

        public static List<Student> GetStudentListInstance()
        {
            if (studentListsInstance == null)
            {
                studentListsInstance = new List<Student>();
            }
            return studentListsInstance;
        }
    }
```


#### IDProvider
Veri tabanı bağlantısı yapmadığım için böyle bir sınıfa ihtiyaç duydum. Her veri listesi için Provider class'larım mevcuttur. Id gibi uniqe bir değeri statik olarak elle vermeyi şık bir yöntem olarak görmediğimden kendi id sağlayıcı sınıflarımı oluşturdum. Burada Random sınıfının bir instance ile rastgele sayılar aldım ve buradaki örnekte olduğuğu gibi her veri listesinin kendi listesinde, üretilmiş bu id nin var olup olmadığını kontrol ettim. Do-while döngüsü ile bir kere id atadıktan sonra check işlemi yapıldı ve Any metodu true döndükçe random bir değer üretilmeye devam edildi. Ta ki üretilen değer mevcut listede bulunmayana kadar.

```c#
public class StudentIDProvider:IIdProvider
    {
        List<Student> studentList = StudentList.GetStudentListInstance();

        Random random = new Random();
        public int GetId()
        {
            int rnd;

            do
            {
                rnd = random.Next(100, 1000);
            }
            while (studentList.Any(r => r.StudentNumber == rnd));

            return rnd;
        }
    }
```

