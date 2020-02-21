using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Dotz.Core.Domain.Models;
using Dotz.Core.Domain.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Dotz.Services
{
  public static class UserPointsControlService
  {
    public static List<UserPointsControl> GenerateUserPoints([FromServices] IUserPointsControlRepository userPointsRepository,
                  Order order)
    {
      List<UserPointsControl> userPointsList = new List<UserPointsControl>();
      foreach (Product item in order.Products)
      {
        //Se o produto é válido para ser resgatado
        if (item.AvaiableToDischarge)
        {
          //Selecionar o total de pontos já acumulados referente ao produto
          userPointsList = userPointsRepository.Get().Where(x => x.ProductId == item.Id &&
                                            x.UserId == order.UserId).ToList();

          //Verificao de total de pontos
          int accumulatedPoints = userPointsList.Sum(x => x.GeneratedPoints);

          //Acumulo novos pontos aos já existentes
          UserPointsControl userPoints = new UserPointsControl();
          userPoints.UserId = order.UserId;
          userPoints.ProductId = item.Id;
          userPoints.GeneratedPoints = item.PointsToAdd;

          //Pode resgatar
          if (accumulatedPoints > item.PointsToDischarge)
            userPoints.CanDischarge = true;
          else
            userPoints.CanDischarge = false;

          //Acumulo novos pontos aos já existentes
          userPointsList.Add(userPoints);
        }
      }
      return userPointsList;
    }
  }
}