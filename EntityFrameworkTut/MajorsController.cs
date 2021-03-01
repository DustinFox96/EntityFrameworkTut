using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkTut.Models;
using System.Linq;


//namespace EntityFrameworkTut {
//   public class MajorsController {
//        private readonly eddbContext _context;// ask becca what this does and why it's needed

//        public IEnumerable<Major> Getall() {
//            return _context.Majors.ToList();
//        }

//        public Major GetByPk(int Id) {
//            return _context.Majors.Find(Id);
//        }

//        public Major Create( Major major) {
//            if(major == null) {
//                throw new Exception("Major cannot be null");
//            }
//            _context.Majors.Add(major);
//            var rowsAffected = _context.SaveChanges();
//            if (rowsAffected != 1) {
//                throw new Exception("Create failed");
//            }
//            return major;
//        }

//        public void Change(Major major) {
//            if(major == null) {
//                throw new Exception("Major cannot be null");
//            }
//            if(major.Id <= 0) {
//                throw new Exception("Major Id must be greater than zero");
//            }
//            _context.Entry(major).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
//            var rowsAffect = _context.SaveChanges();
//            if(rowsAffect != 1) {
//                throw new Exception("Change failed");
//            }
//            return;
//        }

//        public async Task(Major) Remove (int id) {
//            var major = await _context.Majors.FindAsync(id);
//            if(major == null) {
//                return null;
//            }
//            int count = await _context.Students(s => s / Majorid == marjor.id).CountSync();
//            int count = await _context.Students.Where(s => s.MajorId == major.ID).CountAsync();
//            if(count >0) {
//                throw new exception("Cannot remove Major. It is a pk to a student ")
//            }
//        }






//        public MajorsController() {
//            _context = new eddbContext();// ask becca why this is needed and what it does.
//        }
//    }
//}
