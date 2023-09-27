namespace MaxOHara.Dto;

public class ResponseDto<T>
{
    public ResponseDto(T t)
    {
        Data = t;
    }
    
    
    public T Data { get; set; }
}