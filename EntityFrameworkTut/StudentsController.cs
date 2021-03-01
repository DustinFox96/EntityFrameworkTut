using System;
using EntityFrameworkTut.Models;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkTut {

    public class StudentsController {
        // read all the students with SAT scorres between 1000 and 1200 inclusive and order the result by descending.
        public IEnumerable<Student> getBySatRange(int lowSat, int HighSat) {
            //method verison
            return _context.Students
                            .Where(s => s.Sat >= lowSat && s.Sat <= HighSat)
                            .OrderByDescending(s => s.Sat)
                            .ToList();// have becca explain what the heck is going on here. could not get the answer or even start it.
            ////Qurey verison
            //return (from s in _context.Students
            //       where s.Sat >= lowSat && s.Sat <= HighSat
            //       orderby s.Sat descending
            //       select s).ToList();
                         

        }
        // how to use async in our getall
        public async Task<IEnumerable<Student>> Getall() {
            return await _context.Students.ToArrayAsync();
        }
        //public IEnumerable<Student> Getall() {// ask becca why Ienumerable is used here and what the heck it means// it holds a collection.
        //    return _context.Students.ToList();
        //}

        public async Task<Student> GetByPk(int id) {// made async 
            return await _context.Students.FindAsync(id);
        }
        //public Student GetByPk(int id) {
        //    return _context.Students.Find(id);// find here is reading by Primary key. you either turn 1 or null for what you are looking for.
        //}
        private readonly eddbContext _context;// when you use readonly you can only set the value in a constructor
        // this means we can use and put it in a foreach loop. we can put generic list or data in it.// this only means you can only read the data. and no other class could have access to this method. This is also the dbcontext instance that allows us to pull info from there.

        // making this method into a async 
        public async Task<Student> Create(Student student) {
            if (student == null) {
                throw new Exception("Student cannot be null!");
            }
            if (student.Id != 0) {
                throw new Exception("student.Id must be zero!");
            }
            _context.Students.Add(student);
            var rowsAffect = await _context.SaveChangesAsync();// it's important to remember to do SaveChanges 
            if (rowsAffect != 1) {
                throw new Exception("Create failed!");
            } 
            return student;
        }
        //// we are doing a create method here to use in programs
        //public Student Create(Student student) {
        //    if(student == null) {
        //        throw new Exception("Student cannot be null!");
        //    }
        //    if(student.Id != 0) {
        //        throw new Exception("student.Id must be zero!");
        //    }
        //    _context.Students.Add(student);
        //    var rowsAffect = _context.SaveChanges();// it's important to remember to do SaveChanges 
        //    if(rowsAffect != 1) {
        //        throw new Exception("Create failed!");
        //    }
        //    return student;
        //}

        // change does not need a return
        // we also took out void where task used to be becasue it needs to be async 
        public async  Task Change(Student student) {
            if (student == null) {
                throw new Exception("Student cannot be null!");
            }
            if (student.Id <= 0) {
                throw new Exception("student.Id must be greater than zero!");
            }
            _context.Entry(student).State = Microsoft.EntityFrameworkCore.EntityState.Modified;// we are telling entity to change the data here and to map it.
            var rowsAffect = await _context.SaveChangesAsync();// it's important to remember to do SaveChanges 
            if (rowsAffect != 1) {
                throw new Exception("Change failed!");
            }
            return;
        }
        // we are doing a delete here// we also made changes here to make this method run async, removing await and async sytax will bring it back to normal but you also need to make changes to the programs as well.
        public async Task<Student> Remove(int Id) {
            var student = _context.Students.Find(Id);
            if(student == null) {
                return null;
            }
            _context.Students.Remove(student);
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1) {
                throw new Exception("Remove failed");
            }
            return student;
        }
                

        public StudentsController() {
            _context = new eddbContext(); // this is to be able to control the data we put in and write it.
 
        }

} 
    }
