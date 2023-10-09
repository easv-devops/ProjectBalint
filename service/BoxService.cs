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
        return _boxRepository.CreateBox(volume, name, color, description);
    }

    public object UpdateBox(int id, int volume, string name, string color, string description)
    {
        return _boxRepository.UpdateBox(id, volume, name, color, description);
    }

    public void DeleteBox(int id)
    {
        var result = _boxRepository.DeleteBox(id);
        if (!result)
            throw new Exception("Could not delete box");
    }
}