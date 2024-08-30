namespace ActivoFijo.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string resourceName, string fieldName, object fieldValue)
            : base($"{resourceName} no encontrado con {fieldName}: '{fieldValue}'")
        {
        }

        public ResourceNotFoundException(string resourceName)
            : base($"No se encontraron registros de {resourceName}")
        {
        }

    }
}
