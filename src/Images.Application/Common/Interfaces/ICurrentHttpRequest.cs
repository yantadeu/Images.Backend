using Images.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Images.Application.Common.Interfaces;
public interface ICurrentHttpRequest
{
    string? GetUserId();
}
