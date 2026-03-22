using System;

namespace library
{
    public class ENProduct
    {
        // ── Atributos privados ──────────────────────────────────────────
        private string _code;
        private string _name;
        private int _amount;
        private float _price;
        private int _category;
        private DateTime _creationDate;

        // ── Propiedades públicas con campo de respaldo ──────────────────
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public float Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public int Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        // ── Constructores ───────────────────────────────────────────────
        public ENProduct() { }

        public ENProduct(string code, string name, int amount, float price,
                         int category, DateTime creationDate)
        {
            _code = code;
            _name = name;
            _amount = amount;
            _price = price;
            _category = category;
            _creationDate = creationDate;
        }

        // ── Métodos públicos (delegan en CADProduct) ────────────────────
        public bool Create()
        {
            CADProduct cad = new CADProduct();
            return cad.Create(this);
        }

        public bool Update()
        {
            CADProduct cad = new CADProduct();
            return cad.Update(this);
        }

        public bool Delete()
        {
            CADProduct cad = new CADProduct();
            return cad.Delete(this);
        }

        public bool Read()
        {
            CADProduct cad = new CADProduct();
            return cad.Read(this);
        }

        public bool ReadFirst()
        {
            CADProduct cad = new CADProduct();
            return cad.ReadFirst(this);
        }

        public bool ReadNext()
        {
            CADProduct cad = new CADProduct();
            return cad.ReadNext(this);
        }

        public bool ReadPrev()
        {
            CADProduct cad = new CADProduct();
            return cad.ReadPrev(this);
        }
    }
}