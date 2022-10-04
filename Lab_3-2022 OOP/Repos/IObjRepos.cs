namespace Lab_3_2022.Repos;


public interface IObjRepos<T>
{
    public void Add(T item);

    public void Delete(T item);

    public T? Find(Predicate<T> predicate);
    
    public List<T>? FindAll(Predicate<T> predicate);

    public void Display(int index);

}