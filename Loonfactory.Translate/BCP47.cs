using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loonfactory.Translate
{
  public record BCP47
  {
    public string Tag { get; init; } = default!;
    public string? Language { get; init; }
    public string? Region { get; init; }
    public string? Description { get; init; }
  }
}
