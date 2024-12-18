create table users (
                       userid serial primary key,
                       fullname varchar(150) not null,
                       email varchar(150) unique not null,
                       phone varchar(20),
                       city varchar(100),
                       createdat timestamp default current_timestamp
                   );

create table skills (
                        skillid serial primary key,
                        userid int references users(userid),
                        title varchar(150) not null,
                        description text,
                        createdat timestamp default current_timestamp
                    );

create type request_status as enum ('pending', 'accepted', 'rejected');

create table requests (
                          requestid serial primary key,
                          fromuserid int references users(userid),
                          touserid int references users(userid),
                          requestedskillid int references skills(skillid),
                          offeredskillid int references skills(skillid),
                          status request_status default 'pending',
                          createdat timestamp default current_timestamp,
                          updatedat timestamp default current_timestamp
                      );
