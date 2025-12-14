using System;

namespace AccessSystem
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("СИСТЕМА РАСЧЕТА УРОВНЯ ДОСТУПА\n");

            try
            {
                TestEnumApproach();
                Console.WriteLine();
                TestOopApproach();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА: {ex.Message}");
            }
        }
        
        static void TestEnumApproach()
        {
            Console.WriteLine("ЧАСТЬ A: ENUM + SWITCH ПОДХОД\n");
            
            var context1 = new AccessContext(); 
            var context2 = new AccessContext(hasTwoFactor: true);
            var context3 = new AccessContext(isSuspicious: true);
            var context4 = new AccessContext(hasTwoFactor: true, isFromTrustedIP: true);
            var context5 = new AccessContext(isSuspicious: true); 
            
            TestEnumCase("Viewer", UserRole.Viewer, context1, 1);
            TestEnumCase("Editor + 2FA", UserRole.Editor, context2, 4);
            TestEnumCase("Manager + Suspicious", UserRole.Manager, context3, 3);
            TestEnumCase("Admin + 2FA + TrustedIP", UserRole.Admin, context4, 9);
            TestEnumCase("GuestLimited + Suspicious", UserRole.GuestLimited, context5, 1);
        }
        
        static void TestEnumCase(string description, UserRole role, AccessContext context, int expected)
        {
            int result = AccessScoreCalculatorEnum.CalculateAccessScore(role, context);
            string status = result == expected ? "ПРОЙДЕН" : "ОШИБКА";
            Console.WriteLine($"{status} {description}: {result} (ожидалось: {expected})");
        }
        
        static void TestOopApproach()
        {
            Console.WriteLine("ЧАСТЬ B: ООП ПОДХОД\n");
            
            Role viewer = new Viewer();
            Role editor = new Editor();
            Role manager = new Manager();
            Role admin = new Admin();
            Role guest = new GuestLimited();
            
            var context1 = new AccessContext();
            var context2 = new AccessContext(hasTwoFactor: true);
            var context3 = new AccessContext(isSuspicious: true);
            var context4 = new AccessContext(hasTwoFactor: true, isFromTrustedIP: true);
            var context5 = new AccessContext(isSuspicious: true);
            
            TestOopCase("Viewer", viewer, context1, 1);
            TestOopCase("Editor + 2FA", editor, context2, 4);
            TestOopCase("Manager + Suspicious", manager, context3, 3);
            TestOopCase("Admin + 2FA + TrustedIP", admin, context4, 9);
            TestOopCase("GuestLimited + Suspicious", guest, context5, 1);
            
            Console.WriteLine("\nДЕМОНСТРАЦИЯ ПОЛИМОРФИЗМА:");
            Role[] roles = { viewer, editor, manager, admin, guest };
            var testContext = new AccessContext(hasTwoFactor: true, isSuspicious: true);
            
            foreach (var role in roles)
            {
                int score = AccessScoreCalculatorOop.CalculateAccessScore(role, testContext);
                Console.WriteLine($"- {role.GetType().Name}: {score} очков доступа");
            }
        }
        
        static void TestOopCase(string description, Role role, AccessContext context, int expected)
        {
            int result = AccessScoreCalculatorOop.CalculateAccessScore(role, context);
            string status = result == expected ? "ПРОЙДЕН" : "ОШИБКА";
            Console.WriteLine($"{status} {description}: {result} (ожидалось: {expected})");
        }
    }
}