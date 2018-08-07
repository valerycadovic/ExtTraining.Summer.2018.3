namespace DataExportLib
{
    public interface IValidator<in T>
    {
        void Validate(T source);
    }
}
