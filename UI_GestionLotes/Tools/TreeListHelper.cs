using DevExpress.XtraTreeList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UI_GestionLotes.Tools
{
    public class TreeListHelper
    {
        TreeList _treeList;
        List<TreeViewOption> _filter;
        List<TreeViewOptionMultiple> _filterMultiple;
        public TreeListHelper(TreeList treeList)
        {
            this._treeList = treeList;
            this._filter = new List<TreeViewOption>();
            this._filterMultiple = new List<TreeViewOptionMultiple>();
        }
        public TreeListHelper() { }

        public TreeListHelper(TreeList treeList, List<TreeViewOption> filter)
        {
            this._treeList = treeList;
            this._filter = filter;
            InitTreeList<TreeViewOption>(GenerateDataSourceSimple());
        }
        public TreeListHelper(TreeList treeList, List<TreeViewOptionMultiple> filter)
        {
            this._treeList = treeList;
            this._filterMultiple = filter;
            InitTreeList<TreeViewOptionMultiple>(GenerateDataSourceMultiple());
        }

        #region Metodos Helper()
        void InitTreeList<T>(BindingList<T> viewOptions)
        {
            _treeList.Appearance.Row.BackColor = Color.Transparent;
            _treeList.Appearance.Empty.BackColor = Color.Transparent;
            _treeList.BackColor = Color.Transparent;
            _treeList.CheckBoxFieldName = "Checked";
            _treeList.TreeViewFieldName = "Descripcion";
            _treeList.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            _treeList.OptionsBehavior.Editable = false;
            _treeList.OptionsBehavior.ReadOnly = true;
            _treeList.OptionsBehavior.AllowRecursiveNodeChecking = true;
            _treeList.NodeCellStyle += OnNodeCellStyle;
            _treeList.BeforeFocusNode += OnBeforeFocusNode;
            var dataSource = viewOptions;
            _treeList.ForceInitialize();
            _treeList.KeyFieldName = "ID";
            _treeList.ParentFieldName = "ParentID";
            _treeList.DataSource = dataSource;


            _treeList.ExpandAll();
        }

        BindingList<TreeViewOption> GenerateDataSourceSimple()
        {
            var _options = new BindingList<TreeViewOption>();
            _filter.ForEach((obj) =>
            {
                _options.Add(new TreeViewOption()
                {
                    ParentID = obj.ParentID,
                    ID = obj.ID,
                    Descripcion = obj.Descripcion,
                    Codigo = obj.Codigo,
                    Checked = false,
                    Data = obj.Data,
                });
            });

            return _options;
        }
        BindingList<TreeViewOptionMultiple> GenerateDataSourceMultiple()
        {
            var _options = new BindingList<TreeViewOptionMultiple>();
            _filterMultiple.ForEach((obj) =>
            {
                _options.Add(new TreeViewOptionMultiple()
                {
                    ParentID = obj.ParentID,
                    ID = obj.ID,
                    Descripcion = obj.Descripcion,
                    Codigo = obj.Codigo,
                    ColumnaA = obj.ColumnaA,
                    ColumnaB = obj.ColumnaB,
                    ColumnaC = obj.ColumnaC,
                    Checked = false,
                    Data = obj.Data,
                });
            });

            return _options;
        }

        void OnBeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            // Deshabilitar; habilitar en otro metodo para evaluar acciones...
            //e.CanFocus = false;
        }
        void OnNodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                //  e.Appearance.FontSizeDelta += 1;
                e.Appearance.FontStyleDelta = FontStyle.Bold;
            }
            if (e.Node.Level == 1 && e.Node.Nodes.Count > 0)
                e.Appearance.FontStyleDelta = FontStyle.Bold;
        }
        #endregion

        public void TreeViewParaDosNodos<T>(List<T> entidadList,
            string ColumnaCod_Padre, string ColumnaDsc_Padre,
            string ColumnaCod_Hijo, string ColumnaDsc_Hijo)
        {
            var view = new DosNodos(ColumnaCod_Padre, ColumnaDsc_Padre,
                ColumnaCod_Hijo, ColumnaDsc_Hijo, this);
            view.GetTreeViews(entidadList);
        }
        public void TreeViewParaTresNodos<T>(List<T> entidadList,
            string ColumnaCod_Abuelo, string ColumnaDsc_Abuelo,
            string ColumnaCod_Padre, string ColumnaDsc_Padre,
            string ColumnaCod_Hijo, string ColumnaDsc_Hijo)
        {
            var view = new TresNodos(ColumnaCod_Abuelo, ColumnaDsc_Abuelo, ColumnaCod_Padre, ColumnaDsc_Padre,
                ColumnaCod_Hijo, ColumnaDsc_Hijo, this);
            view.GetTreeViews(entidadList);
        }
        public void TreeViewParaCuatroNodos<T>(List<T> entidadList,
            string ColumnaCod_Abuelo, string ColumnaDsc_Abuelo,
            string ColumnaCod_Padre, string ColumnaDsc_Padre,
            string ColumnaCod_Hijo, string ColumnaDsc_Hijo,
            string ColumnaCod_Nieto, string ColumnaDsc_Nieto)
        {
            var view = new CuatroNodos(ColumnaCod_Abuelo, ColumnaDsc_Abuelo, ColumnaCod_Padre, ColumnaDsc_Padre,
                ColumnaCod_Hijo, ColumnaDsc_Hijo, ColumnaCod_Nieto, ColumnaDsc_Nieto, this);
            view.GetTreeViews(entidadList);
        }

        public void TreeViewParaDosNodosMulticolumna<T>(List<T> entidadList,
           string ColumnaCod_Padre, string ColumnaDsc_Padre,
           string ColumnaCod_Hijo, string ColumnaDsc_Hijo,
           string Columna_A = "", string Columna_B = "", string Columna_C = "")
        {
            var view = new DosNodosMulticolumna(ColumnaCod_Padre, ColumnaDsc_Padre,
                ColumnaCod_Hijo, ColumnaDsc_Hijo, Columna_A, Columna_B, Columna_C, this);
            view.GetTreeViews(entidadList);
        }
        public void TreeViewParaTresNodosMulticolumna<T>(List<T> entidadList,
           string ColumnaCod_Abuelo, string ColumnaDsc_Abuelo,
           string ColumnaCod_Padre, string ColumnaDsc_Padre,
           string ColumnaCod_Hijo, string ColumnaDsc_Hijo,
           string Columna_A = "", string Columna_B = "", string Columna_C = "")
        {
            var view = new TresNodosMulticolumna(ColumnaCod_Abuelo, ColumnaDsc_Abuelo, ColumnaCod_Padre, ColumnaDsc_Padre,
                ColumnaCod_Hijo, ColumnaDsc_Hijo, Columna_A, Columna_B, Columna_C, this);
            view.GetTreeViews(entidadList);
        }

        public void CheckTodosLosNodos()
        {
            _treeList.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
            for (int i = 0; i < _treeList.Nodes.Count; i++)
            {
                _treeList.Nodes[i].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                for (int j = 0; j < _treeList.Nodes[i].Nodes.Count(); j++)
                {
                    _treeList.Nodes[i].Nodes[j].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                }
            }
        }
        public void CheckSubNodos()
        {
            for (int i = 0; i < _treeList.Nodes.Count; i++)
            {
                _treeList.Nodes[i].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                for (int j = 0; j < _treeList.Nodes[i].Nodes.Count(); j++)
                {
                    _treeList.Nodes[i].Nodes[j].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                }
            }
        }

        public void CheckUltimoNodoDeTres()
        {
            for (int i = 0; i < _treeList.Nodes.Count; i++)
            {
                for (int j = 0; j < _treeList.Nodes[i].Nodes.Count(); j++)
                {
                    _treeList.Nodes[i].Nodes[j].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                }
            }
        }
        public void CheckUltimoNodoDeDos()
        {
            for (int i = 0; i < _treeList.Nodes.Count; i++)
            {
                _treeList.Nodes[i].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
            }
        }

        public void RadioPrimerNodo()
        {
            // por trabajar
            _treeList.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Radio;
            _treeList.Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Radio;
        }
        #region Clases de Nodos
        private class CuatroNodos
        {
            TreeListHelper _helper;
            public CuatroNodos(
                string ColumnaCod_Abuelo,
                string ColumnaDsc_Abuelo,
                string ColumnaCod_Padre,
                string ColumnaDsc_Padre,
                string ColumnaCod_Hijo,
                string ColumnaDsc_Hijo,
                string ColumnaCodNieto,
                string ColumnaDscNieto,
                TreeListHelper helper
                )
            {
                this._columnaCodAbuelo = ColumnaCod_Abuelo;
                this._columnaDscAbuelo = ColumnaDsc_Abuelo;
                this._columnaCodPadre = ColumnaCod_Padre;
                this._columnaDscPadre = ColumnaDsc_Padre;
                this._columnaCodHijo = ColumnaCod_Hijo;
                this._columnaDscHijo = ColumnaDsc_Hijo;
                this._columnaCodNieto = ColumnaCodNieto;
                this._columnaDscNieto = ColumnaDscNieto;
                this._helper = helper;
            }
            #region Atributos 
            private string _columnaCodAbuelo;
            private string _columnaDscAbuelo;
            private string _columnaCodPadre;
            private string _columnaDscPadre;
            private string _columnaCodHijo;
            private string _columnaDscHijo;
            private string _columnaCodNieto;
            private string _columnaDscNieto;

            private string _valorCodAbuelo;
            private string _valorDscAbuelo;
            private string _valorCodPadre;
            private string _valorDscPadre;
            private string _valorCodHijo;
            private string _valorDscHijo;
            private string _valorCodNieto;
            private string _valorDscNieto;

            #endregion
            #region Metodos
            internal void GetTreeViews<T>(List<T> entidadList)
            {
                var objView = new List<TreeViewOption>();
                if (entidadList.Count() > 0 && entidadList != null)
                {
                    string indicadorNodoAbuelo = string.Empty;
                    string indicadorNodoPadre = string.Empty;
                    string indicadorNodoHijo = string.Empty;
                    int next = 0, contadorHijo = 0, contadorPadre = 0, contadorAbuelo = 0;
                    entidadList.ForEach((parametro) =>
                    {
                        parametro.GetType().GetTypeInfo()
                        .GetProperties().ToList().ForEach((propiedad) =>
                        {
                            if (propiedad.Name.ToString().Equals(_columnaCodAbuelo))
                            {
                                if (propiedad.GetValue(parametro).ToString() != null)
                                    _valorCodAbuelo = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaDscAbuelo))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorDscAbuelo = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaCodPadre))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorCodPadre = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaDscPadre))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorDscPadre = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaCodHijo))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorCodHijo = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaDscHijo))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorDscHijo = propiedad.GetValue(parametro).ToString();
                            }

                            if (propiedad.Name.ToString().Equals(_columnaCodNieto))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorCodNieto = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaDscNieto))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorDscNieto = propiedad.GetValue(parametro).ToString();
                            }
                        });

                        next++;

                        if (!indicadorNodoAbuelo.Equals(_valorDscAbuelo) && _valorDscAbuelo.ToString().Trim() != "")
                        {
                            objView.Add(new TreeViewOption()
                            {
                                ParentID = 0,
                                ID = next,
                                Codigo = _valorCodAbuelo,
                                Descripcion = _valorDscAbuelo,
                                Data = parametro
                            });
                            contadorAbuelo = next;
                            next++;
                        }

                        indicadorNodoAbuelo = _valorDscAbuelo;
                        if (!indicadorNodoPadre.Equals(_valorDscPadre) && _valorDscPadre.ToString().Trim() != "")
                        {
                            objView.Add(new TreeViewOption()
                            {
                                ParentID = contadorAbuelo,
                                ID = next,
                                Codigo = _valorCodPadre,
                                Descripcion = _valorDscPadre,
                                Data = parametro
                            });
                            contadorPadre = next;
                            next++;
                        }

                        indicadorNodoPadre = _valorDscPadre;
                        if (!indicadorNodoHijo.Equals(_valorDscHijo) && _valorDscHijo.ToString().Trim() != "")
                        {
                            objView.Add(new TreeViewOption()
                            {
                                ParentID = contadorPadre,
                                ID = next,
                                Codigo = _valorCodHijo,
                                Descripcion = _valorDscHijo,
                                Data = parametro
                            });
                            contadorHijo = next;
                            next++;
                        }

                        indicadorNodoHijo = _valorDscHijo;

                        if (_valorDscNieto.ToString().Trim() != "")
                        {
                            objView.Add(new TreeViewOption()
                            {
                                ParentID = contadorHijo,
                                ID = next,
                                Codigo = _valorCodNieto,
                                Descripcion = _valorDscNieto,
                                Data = parametro
                            });
                        }
                    });
                }
                _helper._filter = objView;
                _helper.InitTreeList<TreeViewOption>(_helper.GenerateDataSourceSimple());
            }
            #endregion
        }
        private class TresNodos
        {
            TreeListHelper _helper;
            public TresNodos(
                string ColumnaCod_Abuelo,
                string ColumnaDsc_Abuelo,
                string ColumnaCod_Padre,
                string ColumnaDsc_Padre,
                string ColumnaCod_Hijo,
                string ColumnaDsc_Hijo,
                TreeListHelper helper
                )
            {
                this._columnaCodAbuelo = ColumnaCod_Abuelo;
                this._columnaDscAbuelo = ColumnaDsc_Abuelo;
                this._columnaCodPadre = ColumnaCod_Padre;
                this._columnaDscPadre = ColumnaDsc_Padre;
                this._columnaCodHijo = ColumnaCod_Hijo;
                this._columnaDscHijo = ColumnaDsc_Hijo;
                this._helper = helper;
            }
            #region Atributos 
            private string _columnaCodAbuelo;
            private string _columnaDscAbuelo;
            private string _columnaCodPadre;
            private string _columnaDscPadre;
            private string _columnaCodHijo;
            private string _columnaDscHijo;

            private string _valorCodAbuelo;
            private string _valorDscAbuelo;
            private string _valorCodPadre;
            private string _valorDscPadre;
            private string _valorCodHijo;
            private string _valorDscHijo;

            #endregion
            #region Metodos
            internal void GetTreeViews<T>(List<T> entidadList)
            {
                var objView = new List<TreeViewOption>();
                if (entidadList.Count() > 0 && entidadList != null)
                {
                    string indicadorNodoUno = string.Empty;
                    string indicadorNodoDos = string.Empty;
                    int next = 0, contadorPadre = 0, contadorAbuelo = 0;
                    entidadList.ForEach((parametro) =>
                    {
                        parametro.GetType().GetTypeInfo()
                        .GetProperties().ToList().ForEach((propiedad) =>
                        {
                            if (propiedad.Name.ToString().Equals(_columnaCodAbuelo))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorCodAbuelo = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaDscAbuelo))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorDscAbuelo = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaCodPadre))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorCodPadre = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaDscPadre))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorDscPadre = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaCodHijo))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorCodHijo = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaDscHijo))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorDscHijo = propiedad.GetValue(parametro).ToString();
                            }
                        });

                        next++;
                        if (!indicadorNodoUno.Equals(_valorDscAbuelo))
                        {
                            objView.Add(new TreeViewOption()
                            {
                                ParentID = 0,
                                ID = next,
                                Codigo = _valorCodAbuelo,
                                Descripcion = _valorDscAbuelo,
                                Data = parametro
                            });
                            contadorAbuelo = next;
                            next++;
                        }

                        indicadorNodoUno = _valorDscAbuelo;
                        if (!indicadorNodoDos.Equals(_valorDscPadre))
                        {
                            objView.Add(new TreeViewOption()
                            {
                                ParentID = contadorAbuelo,
                                ID = next,
                                Codigo = _valorCodPadre,
                                Descripcion = _valorDscPadre,
                                Data = parametro
                            });
                            contadorPadre = next;
                            next++;
                        }

                        indicadorNodoDos = _valorDscPadre;
                        objView.Add(new TreeViewOption()
                        {
                            ParentID = contadorPadre,
                            ID = next,
                            Codigo = _valorCodHijo,
                            Descripcion = _valorDscHijo,
                            Data = parametro
                        });
                    });
                }
                _helper._filter = objView;
                _helper.InitTreeList<TreeViewOption>(_helper.GenerateDataSourceSimple());
            }
            #endregion

        }
        private class DosNodos
        {
            TreeListHelper _helper;
            public DosNodos(string ColumnaCod_Padre,
                string ColumnaDsc_Padre,
                string ColumnaCod_Hijo,
                string ColumnaDsc_Hijo,
                TreeListHelper helper
                )
            {
                this._columnaCodPadre = ColumnaCod_Padre;
                this._columnaDscPadre = ColumnaDsc_Padre;
                this._columnaCodHijo = ColumnaCod_Hijo;
                this._columnaDscHijo = ColumnaDsc_Hijo;
                this._helper = helper;
            }
            #region Atributos 
            private string _columnaCodPadre;
            private string _columnaDscPadre;
            private string _columnaCodHijo;
            private string _columnaDscHijo;

            private string _valorCodPadre;
            private string _valorDscPadre;
            private string _valorCodHijo;
            private string _valorDscHijo;

            #endregion
            #region Metodos
            internal void GetTreeViews<T>(List<T> entidadList)
            {
                var objView = new List<TreeViewOption>();
                if (entidadList.Count() > 0 && entidadList != null)
                {
                    string indicadorNodoUno = string.Empty;
                    int next = 0, contadorPadre = 0;
                    entidadList.ForEach((parametro) =>
                    {
                        parametro.GetType().GetTypeInfo()
                        .GetProperties().ToList().ForEach((propiedad) =>
                        {
                            if (propiedad.Name.ToString().Equals(_columnaCodPadre))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorCodPadre = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaDscPadre))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorDscPadre = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaCodHijo))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorCodHijo = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaDscHijo))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorDscHijo = propiedad.GetValue(parametro).ToString();
                            }
                        });

                        next++;
                        if (!indicadorNodoUno.Equals(_valorDscPadre))
                        {
                            objView.Add(new TreeViewOption()
                            {
                                ParentID = 0,
                                ID = next,
                                Codigo = _valorCodPadre,
                                Descripcion = _valorDscPadre,
                                Data = parametro
                            });
                            contadorPadre = next;
                            next++;
                        }
                        indicadorNodoUno = _valorDscPadre;
                        objView.Add(new TreeViewOption()
                        {
                            ParentID = contadorPadre,
                            ID = next,
                            Codigo = _valorCodHijo,
                            Descripcion = _valorDscHijo,
                            Data = parametro
                        });
                    });
                }
                _helper._filter = objView;
                _helper.InitTreeList<TreeViewOption>(_helper.GenerateDataSourceSimple());
            }
            #endregion
        }
        private class DosNodosMulticolumna
        {
            TreeListHelper _helper;
            public DosNodosMulticolumna(string ColumnaCod_Padre,
                string ColumnaDsc_Padre,
                string ColumnaCod_Hijo,
                string ColumnaDsc_Hijo,
                string Columna_A,
                string Columna_B,
                string Columna_C,
                TreeListHelper helper
                )
            {
                this._columnaCodPadre = ColumnaCod_Padre;
                this._columnaDscPadre = ColumnaDsc_Padre;
                this._columnaCodHijo = ColumnaCod_Hijo;
                this._columnaDscHijo = ColumnaDsc_Hijo;
                this._columnaA = Columna_A;
                this._columnaB = Columna_B;
                this._columnaC = Columna_C;
                this._helper = helper;
            }
            #region Atributos 
            private string _columnaCodPadre;
            private string _columnaDscPadre;
            private string _columnaCodHijo;
            private string _columnaDscHijo;
            private string _columnaA;
            private string _columnaB;
            private string _columnaC;

            private string _valorCodPadre;
            private string _valorDscPadre;
            private string _valorCodHijo;
            private string _valorDscHijo;
            private string _valorColumnaA;
            private string _valorColumnaB;
            private string _valorColumnaC;
            #endregion
            #region Metodos
            internal void GetTreeViews<T>(List<T> entidadList)
            {
                var objView = new List<TreeViewOptionMultiple>();
                if (entidadList.Count() > 0 && entidadList != null)
                {
                    string indicadorNodoUno = string.Empty;
                    int next = 0, contadorPadre = 0;
                    entidadList.ForEach((parametro) =>
                    {
                        parametro.GetType().GetTypeInfo()
                        .GetProperties().ToList().ForEach((propiedad) =>
                        {
                            if (propiedad.Name.ToString().Equals(_columnaCodPadre))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorCodPadre = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaDscPadre))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorDscPadre = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaCodHijo))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorCodHijo = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaDscHijo))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorDscHijo = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaA))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorColumnaA = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaB))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorColumnaB = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaC))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorColumnaC = propiedad.GetValue(parametro).ToString();
                            }
                        });

                        next++;
                        if (!indicadorNodoUno.Equals(_valorDscPadre))
                        {
                            objView.Add(new TreeViewOptionMultiple()
                            {
                                ParentID = 0,
                                ID = next,
                                Codigo = _valorCodPadre,
                                Descripcion = _valorDscPadre,
                                ColumnaA = "",
                                ColumnaB = "",
                                ColumnaC = "",
                                Data = parametro,
                            });
                            contadorPadre = next;
                            next++;
                        }
                        indicadorNodoUno = _valorDscPadre;
                        objView.Add(new TreeViewOptionMultiple()
                        {
                            ParentID = contadorPadre,
                            ID = next,
                            Codigo = _valorCodHijo,
                            Descripcion = _valorDscHijo,
                            ColumnaA = _valorColumnaA,
                            ColumnaB = _valorColumnaB,
                            ColumnaC = _valorColumnaC,
                            Data = parametro
                        });
                    });
                }
                _helper._filterMultiple = objView;
                _helper.InitTreeList<TreeViewOptionMultiple>(_helper.GenerateDataSourceMultiple());
            }
            #endregion
        }
        private class TresNodosMulticolumna
        {
            TreeListHelper _helper;
            public TresNodosMulticolumna(
                string ColumnaCod_Abuelo,
                string ColumnaDsc_Abuelo,
                string ColumnaCod_Padre,
                string ColumnaDsc_Padre,
                string ColumnaCod_Hijo,
                string ColumnaDsc_Hijo,
                string Columna_A,
                string Columna_B,
                string Columna_C,
                TreeListHelper helper
                )
            {
                this._columnaCodAbuelo = ColumnaCod_Abuelo;
                this._columnaDscAbuelo = ColumnaDsc_Abuelo;
                this._columnaCodPadre = ColumnaCod_Padre;
                this._columnaDscPadre = ColumnaDsc_Padre;
                this._columnaCodHijo = ColumnaCod_Hijo;
                this._columnaDscHijo = ColumnaDsc_Hijo;
                this._columnaA = Columna_A;
                this._columnaB = Columna_B;
                this._columnaC = Columna_C;
                this._helper = helper;
            }
            #region Atributos 
            private string _columnaCodAbuelo;
            private string _columnaDscAbuelo;
            private string _columnaCodPadre;
            private string _columnaDscPadre;
            private string _columnaCodHijo;
            private string _columnaDscHijo;
            private string _columnaA;
            private string _columnaB;
            private string _columnaC;

            private string _valorCodAbuelo;
            private string _valorDscAbuelo;
            private string _valorCodPadre;
            private string _valorDscPadre;
            private string _valorCodHijo;
            private string _valorDscHijo;
            private string _valorColumnaA;
            private string _valorColumnaB;
            private string _valorColumnaC;

            #endregion
            #region Metodos
            internal void GetTreeViews<T>(List<T> entidadList)
            {
                var objView = new List<TreeViewOptionMultiple>();
                if (entidadList.Count() > 0 && entidadList != null)
                {
                    string indicadorNodoUno = string.Empty;
                    string indicadorNodoDos = string.Empty;
                    int next = 0, contadorPadre = 0, contadorAbuelo = 0;
                    entidadList.ForEach((parametro) =>
                    {
                        parametro.GetType().GetTypeInfo()
                        .GetProperties().ToList().ForEach((propiedad) =>
                        {
                            if (propiedad.Name.ToString().Equals(_columnaCodAbuelo))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorCodAbuelo = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaDscAbuelo))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorDscAbuelo = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaCodPadre))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorCodPadre = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaDscPadre))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorDscPadre = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaCodHijo))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorCodHijo = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaDscHijo))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorDscHijo = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaA))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorColumnaA = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaB))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorColumnaB = propiedad.GetValue(parametro).ToString();
                            }
                            if (propiedad.Name.ToString().Equals(_columnaC))
                            {
                                if (propiedad.GetValue(parametro) != null)
                                    _valorColumnaC = propiedad.GetValue(parametro).ToString();
                            }
                        });

                        next++;
                        if (!indicadorNodoUno.Equals(_valorDscAbuelo))
                        {
                            objView.Add(new TreeViewOptionMultiple()
                            {
                                ParentID = 0,
                                ID = next,
                                Codigo = _valorCodAbuelo,
                                Descripcion = _valorDscAbuelo,
                                ColumnaA = "",
                                ColumnaB = "",
                                ColumnaC = "",
                                Data = parametro
                            });
                            contadorAbuelo = next;
                            next++;
                        }

                        indicadorNodoUno = _valorDscAbuelo;
                        if (!indicadorNodoDos.Equals(_valorDscPadre))
                        {
                            objView.Add(new TreeViewOptionMultiple()
                            {
                                ParentID = contadorAbuelo,
                                ID = next,
                                Codigo = _valorCodPadre,
                                Descripcion = _valorDscPadre,
                                ColumnaA = _valorColumnaA,
                                ColumnaB = _valorColumnaB,
                                ColumnaC = _valorColumnaC,
                                Data = parametro
                            });
                            contadorPadre = next;
                            next++;
                        }

                        indicadorNodoDos = _valorDscPadre;
                        objView.Add(new TreeViewOptionMultiple()
                        {
                            ParentID = contadorPadre,
                            ID = next,
                            Codigo = _valorCodHijo,
                            Descripcion = _valorDscHijo,
                            ColumnaA = _valorColumnaA,
                            ColumnaB = _valorColumnaB,
                            ColumnaC = _valorColumnaC,
                            Data = parametro
                        });
                    });
                }
                _helper._filterMultiple = objView;
                _helper.InitTreeList<TreeViewOptionMultiple>(_helper.GenerateDataSourceMultiple());
            }
            #endregion

        }
        public class TreeViewOption : INotifyPropertyChanged
        {
            public int ParentID { get; set; }
            public int ID { get; set; }
            public string Codigo { get; set; }
            public string Descripcion { get; set; }
            public object Data { get; set; }
            bool? checkedCore = false;
            public bool? Checked
            {
                get { return checkedCore; }
                set
                {
                    if (checkedCore == value)
                        return;
                    checkedCore = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Checked"));
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
        }
        public class TreeViewOptionMultiple : INotifyPropertyChanged
        {
            public int ParentID { get; set; }
            public int ID { get; set; }
            public string Codigo { get; set; }
            public string Descripcion { get; set; }
            public string ColumnaA { get; set; }
            public string ColumnaB { get; set; }
            public string ColumnaC { get; set; }
            public object Data { get; set; }
            bool? checkedCore = false;
            public bool? Checked
            {
                get { return checkedCore; }
                set
                {
                    if (checkedCore == value)
                        return;
                    checkedCore = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Checked"));
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
        }
        #endregion


        /// <summary>
        /// Método que retorna, los códigos de la entidad alojada en los Nodos, 
        /// concatenado y separado por una ",". Necesita el listado de los Nodos Checados.
        /// </summary>
        /// <param name="listNodes">Lista de TreeListNode</param>
        /// <returns></returns>
        public string ObtenerCodigoConcatenadoDeNodo()
        {
            var trabChecked = _treeList.GetAllCheckedNodes();

            string split = "delete";
            trabChecked.ForEach((tr) =>
            {
                //if (!tr.HasChildren)
                {
                    split += $",{tr.GetValue("Codigo")}";
                }
            });
            split = split.Replace("delete,", "");
            return split.Trim();
        }


        public string ObtenerCodigoConcatenadoDeNodoNieto()
        {
            var trabChecked = _treeList.GetAllCheckedNodes();

            string split = "delete";
            trabChecked.ForEach((tr) =>
            {
                if (!tr.HasChildren)
                {
                    split += $",{tr.GetValue("Codigo")}";
                }
            });
            split = split.Replace("delete,", "");
            return split.Trim();
        }

        /// <summary>
        /// Obtiene los Código concatenados y separados por coma; esto de un tree_list/view.
        /// Como parámetro necesita el indice del nodo, inicia en 0.
        /// </summary>
        /// <param name="indice">Indice de los nodos: inicia en 0</param>
        /// <returns></returns>
        public string ObtenerCodigoConcatenadoDeNodoIndex(int indice)
        {
            var trabChecked = _treeList.GetAllCheckedNodes();



            string split = "delete";
            trabChecked.ForEach((tr) =>
            {
                if (tr.Level == indice)
                    split += $",{tr.GetValue("Codigo")}";
            });
            split = split.Replace("delete,", "");
            return split.Trim();
        }

        public string[]  ObtenerCodigoConcatenadoDeNodoArray(int indice)
        {
            var trabChecked = _treeList.GetAllCheckedNodes();



            string split = "delete";
            trabChecked.ForEach((tr) =>
            {
                if (tr.Level == indice)
                    split += $",{tr.GetValue("Codigo")}";
            });
            split = split.Replace("delete,", "");
            var array = split.Split(',');

            string[] salida = new string[5];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Contains("|"))
                {
                    salida[0] += $"{array[i]},";
                }
                else if (array[i].Length == 2)
                {
                    salida[1] = array[i];
                }
                else
                {
                    if (ContieneTipoContacto(array[i]))
                    {
                        salida[2] += $"{array[i]},";
                    }
                    else if (ContieneTipoCanal(array[i]))
                    {
                        salida[3] += $"{array[i]},";
                    }
                    else
                    {
                        salida[4] += $"{array[i]},";
                    }
                }

            }
            salida[0] = salida[0] == null ? "" : salida[0].Substring(0, salida[0].Length - 1);
            salida[2] = salida[2] == null ? "" : salida[2].Substring(0, salida[2].Length - 1);
            salida[3] = salida[3] == null ? "" : salida[3].Substring(0, salida[3].Length - 1);
            salida[4] = salida[4] == null ? "" : salida[4].Substring(0, salida[4].Length - 1);

            return salida;
        }

        bool ContieneSoloNumero(string value)
        {
            var ff = Regex.IsMatch(value, @"^\d+$");
            return ff;
        }
        bool ContieneNumero(string value)
        {
            var ff = Regex.IsMatch(value, @"\d");
            return ff;
        }

        bool ContieneTipoContacto(string value)
        {
            var contiene = value.Contains("TPCON");
            return contiene;
        }
        bool ContieneTipoCanal(string value)
        {
            var contiene = value.Contains("CAPRO");
            return contiene;
        }
        private int validarCantidadNumerosCadena(string campoValidar)
        {
            string str = campoValidar;
            List<string> chars = new List<string>();
            chars.AddRange(str.Select(c => c.ToString()));


            int n = 0;
            foreach (var a in chars)
            {
                int[] k = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                foreach (int b in k)
                {
                    if (b.ToString() == a) n += 1;
                }
            }
            return n;
        }
        // 'sadasd,asdasd,sdasd'

        /// <summary>
        /// Método que retorna, los códigos de una lista, 
        /// concatenado y separado por una ",". un listado de cualquier clase
        /// y que se indique el nombre de la columna/atributo a concatenar.
        /// </summary>
        /// <typeparam name="T">Entidad</typeparam>
        /// <param name="objList">Listado de la Entidad</param>
        /// <param name="cod_list">Columna a concatenar</param>
        /// <returns></returns>
        public string ObtenerValoresConcatenadoDeLista<T>(List<T> objList, string cod_list)
        {

            string split = "delete";
            objList.ForEach((obj) =>
            {
                var properties = obj.GetType().GetTypeInfo().GetProperties();
                properties.ToList()
                .ForEach((k) =>
                {
                    if (k.Name.ToString().Equals(cod_list))
                    {
                        var value = k.GetValue(obj);
                        if (value != null)
                        {
                            split += $",{value}";
                        }
                    }
                });
            });
            split = split.Replace("delete,", "");
            return split.Trim();
        }
    }
}
