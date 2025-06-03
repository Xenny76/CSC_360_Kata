using CSC160_Final.CellUpdateStrategies;
using CSC160_Final.Interfaces;
namespace CSC160_Final
{
    public class CellUpdateStrategyFactory : ICellUpdateStrategyFactory<ICellUpdateStrategy>
    {
        public ICellUpdateStrategy CreateStrategy(string strategyName)
        {
            ICellUpdateStrategy strategy = strategyName switch
            {
                "High Life" => new HighLifeUpdateStrategy(),
                "Day and Night" => new DayAndNightUpdateStrategy(),
                _ => new ConwayUpdateStrategy(),
            };
            return strategy;
        }
    }
}