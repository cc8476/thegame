//覆盖父类 
protected override void Start()
{
    base.Start();
}



//强制转换
double c = 10.5;
int d = (int)c;//显示转换



//数组，不能修改数组长度
int[] myArray = {1，3，5，7};
int v1 = myArray[0];


Person[] myPersons = new Person[2];


for (int i = 0; i < myArr.Length; i++)  {
    Console.Write("{0}->", myArr[i]);
}  




在C#中，只能在动态数组ArrayList类中向数组添加元素。因为动态数组是一个可以改变数组长度和元素个数的数据类型。



ArrayList al = new ArrayList();

al.Add(45);
al.Add(78);
al.Add(33);
al.Add(56);

al.Remove(3);//移除值为3的
al.RemoveAt(3);//移除第3个

Console.WriteLine("Capacity: {0} ", al.Capacity);//容量，会自动增加
Console.WriteLine("Count: {0}", al.Count); //实际个数
            
foreach (int i in al)
{
    Console.Write(i + " ");
}

al.Sort();


Add方法用于添加一个元素到当前列表的末尾
AddRange方法用于添加一批元素到当前列表的末尾
Remove方法用于删除一个元素，通过元素本身的引用来删除
RemoveAt方法用于删除一个元素，通过索引值来删除
RemoveRange用于删除一批元素，通过指定开始的索引和删除的数量来删除
Insert用于添加一个元素到指定位置，列表后面的元素依次往后移动
InsertRange用于从指定位置开始添加一批元素，列表后面的元素依次往后移动



### 反射类的实例的属性：
Type t = model.GetType();
PropertyInfo[] PropertyList = t.GetProperties();


foreach (PropertyInfo item in PropertyList)
{
    string name = item.Name;
    object value = item.GetValue(model, null);

    Debug.Log("FFFFF:" + name + "," + value);
}

c#的class中，属性和成员变量是2个概念

   public string name;//  这个是成员变量
   public string headpic { get; set; }//  这个才是属性，只有这个能遍历


   Type t = model.GetType();
   PropertyInfo[] PropertyList = t.GetProperties();

   当然，还有对应t.GetMembers();//但是会打印出来所有的Member,包括系统给出的，这样就不好遍历了


父类定义 virtual
    public virtual object getDataById(int id)
    {
        return new object();
    }

子类才可以override
public override object getDataById(int id)
        {
            
        }



泛型List 


        public List<RawRoleStructure> getAllData()
        {

            List<RawRoleStructure> list = new List<RawRoleStructure>();
            return list;
        }



协程：
StartCoroutine在unity3d的帮助中叫做协程，意思就是启动一个辅助的线程。
在C#中直接有Thread这个线程，但是在unity中有些元素是不能操作的。这个时候可以使用协程来完成。
使用线程的好处就是不会出现界面卡死的情况，如果有一次非常大量的运算，没用线程就会出现假死的情况。

StartCoroutine(api()); //类似于js中的worker


StartCoroutine("ReloadGame");

这个方法是协程的写法，在C#中协程要定义为IEnumerator 这个类型
IEnumerator ReloadGame()
{			
    // ... pause briefly
    yield return new WaitForSeconds(2);  // 看上去只有在协程中使用
    // ... and then reload the level.
    Application.LoadLevel(Application.loadedLevel);
}



给代码增加提示
[Tooltip("0=role , 1=enemy")]
public int chartype;


复杂的协程+返回值：
   public RawImage imageBox;
    void Start()
    {
        StartCoroutine(LoadTexture2D("/icon.png", (value) => imageBox.texture = value));
    }

    public IEnumerator LoadTexture2D(string url, Action<Texture2D> taskCompletedCallBack)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(Application.persistentDataPath + url);
        yield return request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError)
        { }
        else
        {
            var texture = DownloadHandlerTexture.GetContent(request);
            taskCompletedCallBack(texture);
        }

    }
