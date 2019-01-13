using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableEditorCore.TableComponents;
using TableEditorCore.TableComponents.Cells;

namespace TableEditorCore.Services
{
    public interface ICellEvaluator
    {
        void Evaluate(ICell cell);
    }
}
