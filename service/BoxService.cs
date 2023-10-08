using System.ComponentModel.DataAnnotations;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using infrastructure.Repositories;

namespace service;

public class BoxService
{
    private readonly BoxRepository _boxRepository;
    
    public BoxService(BoxRepository boxRepository)
    {
        _boxRepository = boxRepository;
    }

    public IEnumerable<BoxFeedQuery> GetBoxesForFeed()
    {
        return _boxRepository.GetBoxesForFeed();
    }

    public object CreateBox(int volume, string name, string color, string description)
    {
        if (true)
        {
            return _boxRepository.CreateBox(volume, name, color, description);
        }

        throw new ValidationException("Box with the name "+ name +" already exists.");
    }

    public Box UpdateBox(int id, int volume, string name, string color, string description)
    {
        return _boxRepository.UpdateBox(id, volume, name, color, description);
    }

    public void DeleteBox(int id)
    {
        _boxRepository.DeleteBox(id);
    }
}
