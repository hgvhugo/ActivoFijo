using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ActivoFijo.Services
{
    public class ExcelChunkReaderService
    {
        public async Task LeerArchivoExcelEnChunks<T>(
            IFormFile archivo,
            int chunkSize,
            List<(int Columna, Action<T, string> AsignarValor)> mapeoColumnas,
            Func<T> crearInstancia,
            Func<List<T>, Task> procesarChunk)
        {
            using (var stream = new MemoryStream())
            {
                await archivo.CopyToAsync(stream);
                stream.Position = 0;

                IWorkbook workbook = new XSSFWorkbook(stream);
                ISheet sheet = workbook.GetSheetAt(0);

                // Validar que la hoja tiene al menos una fila de encabezado
                if (sheet.LastRowNum < 1)
                {
                    throw new InvalidOperationException("El archivo Excel no contiene suficientes filas.");
                }

                // Validar que la fila de encabezado tiene suficientes columnas
                IRow headerRow = sheet.GetRow(0);
                if (headerRow == null || headerRow.LastCellNum < mapeoColumnas.Count)
                {
                    throw new InvalidOperationException("El archivo Excel no contiene suficientes columnas.");
                }


                int totalRows = sheet.LastRowNum;
                var tasks = new List<Task>();

                for (int startRow = 1; startRow <= totalRows; startRow += chunkSize)
                {
                    int endRow = Math.Min(startRow + chunkSize - 1, totalRows);
                    List<T> chunk = new List<T>();

                    for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
                    {
                        IRow row = sheet.GetRow(rowIndex);
                        if (row != null && !EsFilaVacia(row))
                        {
                            T instancia = crearInstancia();
                            foreach (var (columna, asignarValor) in mapeoColumnas)
                            {
                                ICell cell = row.GetCell(columna);
                                if (cell != null)
                                {
                                    // Validar el valor de la celda antes de asignarlo
                                    string valor = cell.ToString();
                                    if (!string.IsNullOrEmpty(valor))
                                    {
                                        try
                                        {
                                            asignarValor(instancia, valor);
                                        }
                                        catch (InvalidCastException ex)
                                        { 
                                            Console.WriteLine($"Error de conversión en la columna {columna}: {ex.Message}");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Advertencia: La celda en la columna {columna} está vacía o no existe.");
                                }
                            }
                            chunk.Add(instancia);
                        }
                    }

                    // Añadir la tarea de procesamiento del chunk a la lista de tareas
                    tasks.Add(procesarChunk(chunk));
                }

                // Esperar a que todas las tareas de procesamiento se completen
                await Task.WhenAll(tasks);
            }
        }

        private bool EsFilaVacia(IRow row)
        {
            foreach (var cell in row.Cells)
            {
                if (cell != null && !string.IsNullOrEmpty(cell.ToString()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
