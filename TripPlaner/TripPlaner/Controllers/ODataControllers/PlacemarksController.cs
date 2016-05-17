using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using TripPlaner.DAL;
using TripPlaner.DAL.Entities;
using TripPlaner.Services.Services;

namespace TripPlaner.Controllers.ODataControllers
{
    [ODataRoutePrefix("Placemarks")]
    public class PlacemarksController : ODataController
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IEntityService<Placemark> _service;

        public PlacemarksController(IUnitOfWork unitOfWork, IEntityService<Placemark> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        /// <summary>
        /// Получение списка
        /// </summary>
        /// <returns></returns>
        [EnableQuery]
        public IQueryable<Placemark> Get()
        {
            return _service.AsQueryable();
        }

        /// <summary>
        /// Получение записи по ключу
        /// </summary>
        /// <param name="key">ключ</param>
        /// <returns></returns>
        [EnableQuery]
        public SingleResult<Placemark> Get([FromODataUri] int key)
        {
            return SingleResult.Create(_service.AsQueryable().Where(_ => _.Id == key));
        }

        /// <summary>
        /// Добавление
        /// </summary>
        /// <param name="data">запись</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(Placemark data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.InsertAndSaveAsync(data);
            return Created(data);
        }

        /// <summary>
        /// Частичное обновление записи
        /// </summary>
        /// <param name="key">ключ</param>
        /// <param name="data">неполная запись</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Placemark> data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _service.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            data.Patch(entity);

            try
            {
                await _unitOfWork.SaveChangeAsync();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                //todo: log
                throw;
            }

            return Updated(entity);
        }

        /// <summary>
        /// Полное обновление записи
        /// </summary>
        /// <param name="key">ключ</param>
        /// <param name="data">запись</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Placemark data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != data.Id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateAndSaveAsync(data);
            }
            catch (Exception)
            {
                //todo: log
                throw;
            }

            return Updated(data);
        }

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="key">ключ</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var entity = await _service.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            await _service.DeleteAndSaveAsync(entity);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}