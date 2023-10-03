using System.Collections.Generic;

namespace APITest.Converter.Interface
{
  public interface IConverter<T, V>
  {
    public T convertDtoToModel(V dtos);
    public V convertModelToDto(T model);
    public List<T> convertListDtoToListModel(List<V> dtos);
    public List<V> convertListModelToListDto(List<T> models);
  }
}