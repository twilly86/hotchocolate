namespace HotChocolate.Execution
{
    public interface IExecutionPlan
    {
        void ExecuteAsync(object context, object variables);
    }

    public interface IExecutionStep
    {

    }


}

