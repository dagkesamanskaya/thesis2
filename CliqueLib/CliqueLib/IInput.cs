using System;
using System.Collections.Generic;

namespace CliqueLib
{
    public interface IInput
    {
        Boolean CheckInconsistency(IInput seq2);

        Boolean? IsWeaker(IInput seq2);

    }

}