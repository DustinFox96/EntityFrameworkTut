using EntityFrameworkTut.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkTut {
    class Program {
        // took our our void in replace for Task in order to make this a aysnc
        static async Task Main(string[] args) { // our main method was marked as async to properly use it down below and to have it be attached to our studentcontroller as well
            var sctrl = new StudentsController();

            // we made this an async 
            var sGreg = new Student {
                Id = 0, Firstname = "Greg", Lastname = "Doud", StateCode = "OH",
                Gpa = 2.1m, Sat = 805, MajorId = 1
            };
            var sGregNew = await sctrl.Create(sGreg);
            Console.WriteLine($"{sGregNew.Id}{sGregNew.Firstname} {sGregNew.Lastname}");

            var std = await sctrl.GetByPk(sGregNew.Id);

            std.Firstname = "Gregory";
             await sctrl.Change(std);

            var studentDel = await sctrl.Remove(std.Id);// have becca explain why this works.



            var st = await sctrl.GetByPk(sGregNew.Id);// the getByPk used to only have a 1 in it's () before we changeed it to what it is now so we could update up above
            if (st == null) {
                Console.WriteLine("Not found");
            } else {
                Console.WriteLine($"{st.Firstname} {st.Lastname}");
            }

             var st1 =  await sctrl.GetByPk(11111);
             if(st == null) {
                Console.WriteLine("Not found");
            } else {
                Console.WriteLine($"{st1.Firstname} {st1.Lastname}");
            }

      

            var students = await sctrl.Getall();// we added await here to make it async
            foreach(var s in students) {
                Console.WriteLine($"{s.Id} {s.Firstname} {s.Lastname}");
            }
        }
        static void Run1() { 
            // this is Method Syntax
            var _context = new eddbContext();
            _context.Students.ToList()
                .ForEach(s => Console.WriteLine($"{s.Firstname} {s.Lastname}"));

            // this is Qurey Syntax
            var majors = from m in _context.Majors
                          where m.MinSat > 1000
                          orderby m.Description
                          select m;
            foreach(var m in majors) {
                Console.WriteLine($"{m.Description} | {m.MinSat}");
            }
            // join Students & Majors. print Name and Major
            var allStudents = (from s in _context.Students
                               join m in _context.Majors.DefaultIfEmpty()
                               on s.MajorId equals m.Id into grp
                               from mm in grp.DefaultIfEmpty()
                               select new {
                                   Name = s.Firstname + " " + s.Lastname,
                                   Major = mm == null ? "undeclared" : mm.Description
                               }).ToList(); // a lot of stuff happening here, have becca or her mom better explain

            allStudents.ForEach(s => Console.WriteLine($"{s.Name} - {s.Major}"));
                        
                       
           
            
            

        }
    }
}
