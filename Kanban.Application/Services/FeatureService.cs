using Kanban.Application.Common.Models;
using Kanban.Application.Common.Statics;
using Kanban.Application.Common.Utils;
using Kanban.Application.Interfaces;
using Kanban.Application.Validators.Users;
using Kanban.Domain.Entities;
using Kanban.Infraestructure.UnitsOfWork;

namespace Kanban.Application.Services
{
  public class FeatureService : BaseService, IFeatureService
  {
    private protected readonly UserValidator _validator;

    public FeatureService(IUnitOfWork unitOfWork, ExceptionsLogger logger, UserValidator validator) : base(unitOfWork, logger)
    {
      _validator = validator;
    }

    public async Task<ResponseData<List<Feature>>> GetFeatures()
    {
      var response = new ResponseData<List<Feature>>();
      try
      {
        response.Data = await _unitOfWork.FeatureRepository.GetFeatures();
        response.Message =
          response.Data.Count > 0
          ? ReplyMessages.QUERY_SUCCESS
          : ReplyMessages.QUERY_EMPTY;
      }
      catch (Exception ex)
      {
        response.Status = StatusResponse.INTERNAL_SERVER_ERROR;
        response.Message = ReplyMessages.QUERY_FAILED;
        _logger.SetException("Error al obtener funcionalidades: " + ex.Message);
      }

      return response;
    }

    public async Task<ResponseData<Feature>> GetFeatureById(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<Response> InsertFeature(Feature feature)
    {
      throw new NotImplementedException();
    }

    public async Task<Response> UpdateFeature(Feature feature)
    {
      throw new NotImplementedException();
    }

    public async Task<Response> DeleteFeature(int id)
    {
      throw new NotImplementedException();
    }
  }
}
