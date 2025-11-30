namespace AccessSystem
{
    public class Viewer : Role
    {
        public Viewer() : base(1) { }
    }

    public class Editor : Role
    {
        public Editor() : base(3) { }
    }

    public class Manager : Role
    {
        public Manager() : base(5) { }
    }

    public class Admin : Role
    {
        public Admin() : base(7) { }
    }

    public class GuestLimited : Role
    {
        public GuestLimited() : base(1) { }
        
        public override int GetAccess(AccessContext context)
        {
            return BasePoints;
        }
    }
}