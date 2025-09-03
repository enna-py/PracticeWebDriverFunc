using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models;
public record User(
        int Id,
        string? Name,
        string? Username,
        string? Email,
        Address? Address,
        string? Phone,
        string? Website,
        Company? Company
    );
