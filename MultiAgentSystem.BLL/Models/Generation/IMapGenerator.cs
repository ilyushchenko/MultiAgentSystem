using System.Collections.Generic;

namespace MultiAgentSystem.BLL.Models.Generation
{
    public interface IMapGenerator
    {
        Cell GetCell(Point position);
    }
}