using Kanban.Application.Common.Models;
using Kanban.Application.Common.Statics;
using Kanban.Application.Common.Utils;
using Kanban.Application.Interfaces;
using Kanban.Domain.Entities;
using Kanban.Infraestructure.Kanban.UnitsOfWork;

namespace Kanban.Application.Services
{
  public class FeatureService : BaseService, IFeatureService
  {
    private readonly IUnitOfWorkKanban _unitOfWork;

    public FeatureService(IUnitOfWorkKanban unitOfWork, ExceptionsLogger logger) : base(logger)
    {
      _unitOfWork = unitOfWork;
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
      var response = new ResponseData<Feature>();
      try
      {
        var Feature = await _unitOfWork.FeatureRepository.GetFeatureById(id);

        if (Feature is null)
        {
          response.Status = StatusResponse.NOT_FOUND;
          response.Message = ReplyMessages.RECORD_NOT_FOUND;
        }
        else
        {
          response.Data = Feature;
          response.Message = ReplyMessages.QUERY_SUCCESS;
        }
      }
      catch (Exception ex)
      {
        response.Status = StatusResponse.INTERNAL_SERVER_ERROR;
        response.Message = ReplyMessages.QUERY_FAILED;
        _logger.SetException("Error al obtener funcionalidad por id: " + ex.Message);
      }

      return response;
    }

    public async Task<Response> InsertFeature(Feature feature)
    {
      var response = new Response();

      try
      {
        await _unitOfWork.FeatureRepository.InsertFeature(feature);
        _unitOfWork.Commit();
        response.Message = ReplyMessages.SAVE;
      }
      catch (Exception ex)
      {
        response.Status = StatusResponse.INTERNAL_SERVER_ERROR;
        response.Message = ReplyMessages.FAILED_OPERATION;
        _logger.SetException("Error al insertar funcionalidad: " + ex.Message);
      }

      return response;
    }

    public async Task<Response> UpdateFeature(Feature feature)
    {
      var response = new Response();
      var currentUser = await _unitOfWork.FeatureRepository.GetFeatureById(feature.Id);

      if (currentUser is null)
      {
        response.Status = StatusResponse.NOT_FOUND;
        response.Message = ReplyMessages.RECORD_NOT_FOUND;
        return response;
      }

      try
      {
        await _unitOfWork.FeatureRepository.UpdateFeature(feature);
        _unitOfWork.Commit();
        response.Message = ReplyMessages.UPDATE;
      }
      catch (Exception ex)
      {
        response.Status = StatusResponse.INTERNAL_SERVER_ERROR;
        response.Message = ReplyMessages.FAILED_OPERATION;
        _logger.SetException("Error al insertar la funcionalidad: " + ex.Message);
      }

      return response;
    }

    public async Task<Response> DeleteFeature(int id)
    {
      var response = new Response();
      var user = await _unitOfWork.FeatureRepository.GetFeatureById(id);

      if (user is null)
      {
        response.Status = StatusResponse.NOT_FOUND;
        response.Message = ReplyMessages.RECORD_NOT_FOUND;
        return response;
      }

      try
      {
        await _unitOfWork.FeatureRepository.DeleteFeature(id);
        _unitOfWork.Commit();
        response.Message = ReplyMessages.DELETE;
      }
      catch (Exception ex)
      {
        response.Status = StatusResponse.INTERNAL_SERVER_ERROR;
        response.Message = ReplyMessages.FAILED_OPERATION;
        _logger.SetException("Error al eliminar la funcionalidad: " + ex.Message);
      }

      return response;
    }
  }
}
