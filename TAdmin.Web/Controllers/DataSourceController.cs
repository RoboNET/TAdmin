using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TAdmin.Core;
using TAdmin.DataSource.Mssql;

namespace TAdmin.Controllers
{
    [ApiController]
    [Route("data")]
    public class DataSourceController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDataSource _dataSource;

        public DataSourceController(ILogger<WeatherForecastController> logger, IDataSource dataSource)
        {
            _logger = logger;
            _dataSource = dataSource;
        }

        [HttpGet]
        public async Task<IDatabase> Get()
        {
            var result = await _dataSource.GetDatabaseInfo();
            return result;
        }
    }
    
    public class TableConverter : JsonConverter<ITable>
    {
        public override ITable Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, ITable value, JsonSerializerOptions options)
        {
            if (value is RelationTable)
                JsonSerializer.Serialize(writer, value as RelationTable, typeof(RelationTable), options);
            else if (value is DocumentTable)
                JsonSerializer.Serialize(writer, value as DocumentTable, typeof(DocumentTable), options);
            else
                throw new ArgumentOutOfRangeException(nameof(value), $"Unknown implementation of the interface {nameof(ITable)} for the parameter {nameof(value)}. Unknown implementation: {value?.GetType().Name}");
        }
    }

    
    public class FieldMetadataConverter : JsonConverter<IFieldMetadata>
    {
        public override IFieldMetadata Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, IFieldMetadata value, JsonSerializerOptions options)
        {
            if (value is RelationFieldMetadata)
                JsonSerializer.Serialize(writer, value as RelationFieldMetadata, typeof(RelationTable), options);
            else
                throw new ArgumentOutOfRangeException(nameof(value), $"Unknown implementation of the interface {nameof(IFieldMetadata)} for the parameter {nameof(value)}. Unknown implementation: {value?.GetType().Name}");
        }
    }
}