    //���ؿ�ҳ�洢Storage��//������4��,����ie6/7/8��firefox2+/3+,chrome,opera9/10,safari���а汾
    //����Storage.Set("�洢������","�洢��Ŀ��","�洢����ֵ");
    //��ȡStorage.Get("�洢������","�洢��Ŀ��");
    //ɾ��Storage.Del("�洢������","�洢��Ŀ��");
    //���Storage.Remove("�洢������");
    //var saveContent = { id: "localCache", name: "userSet", value: "xxxxx" };
    //Storage.Set(saveContent.id, saveContent.name, saveContent.value);
    
    
    (function() {
        window.Storage = (function() {
            var o = {
                ls: (typeof window.localStorage == "object") ? true : false, //��֧��localstorage
                u: (navigator.appVersion.toString().indexOf("MSIE") > 0
                && navigator.appVersion.toString().indexOf("MSIE 9.0") < 0) ? true : false,
                //ie9���°汾userData
                g: (window.google && google.gears && google.gears.factory.create) ? true : false, //��gears��֧��gears
                gs: (typeof window.globalStorage == "object") ? true : false //��֧��globalStorage
            };

            //gears����
            var g = (function() {
                var db = (o.g) ? google.gears.factory.create("beta.database", "1.0") : null;
                return {
                    Set: function(name, key, val) {
                        var complete = false;
                        db.open("beta.database");
                        with (db) try {
                            execute("create table if not exists storage" + name + " (itemkey text unique not null PRIMARY key,itemval text not null)");
                            var rs = execute("select itemkey from storage" + name + " where itemkey=?", [key]);
                            if (rs.isValidRow()) { execute("update storage" + name + " set itemval=? where itemkey=?", [val, key]); } else { execute("insert into storage" + name + " values (?,?)", [key, val]); };
                            rs.close();
                            complete = true;
                        } catch (e) { }; //alert(e.message);
                        db.close();
                        return complete;
                    },
                    Get: function(name, key) {
                        var backval = "";
                        db.open("beta.database");
                        try {
                            var rs = db.execute("select itemkey,itemval from storage" + name + " where itemkey=?", [key]);
                            if (rs.isValidRow()) {
                                backval = rs.field(1);
                                backval = (backval == null || backval == undefined) ? "" : backval;
                            }
                            rs.close();
                        } catch (e) { };
                        db.close();
                        return backval;
                    },
                    Del: function(name, key) {
                        var complete = false;
                        db.open("beta.database");
                        try {
                            db.execute("delete from storage" + name + " where itemkey=?", [key]);
                            complete = true;
                        } catch (e) { };
                        db.close();
                        return complete;
                    },
                    Remove: function(name) {
                        var complete = false;
                        db.open("beta.database");
                        try {
                            db.execute("drop table storage" + name);
                            complete = true;
                        } catch (e) { };
                        close();
                        return complete;
                    }
                }
            })();

            //localStorage��globalStorage����(��������firefox,chrome,opear,ie8/9֧��localStorage)
            var gs, ls;
            gs = ls = (function() {
                var storage = (o.ls) ? window.localStorage : (o.gs) ? window.globalStorage[location.hostname] : null;
                return {
                    Set: function(name, key, val) {
                        var loadval = storage.getItem(name);
                        if (loadval == undefined || loadval == null) {
                            storage.setItem(name, key + "=" + val);
                        } else {
                            var array = loadval.split(";");
                            var len = array.length;
                            var addnew = true;
                            for (var i = 0; i < len; i++) {
                                if (array[i].indexOf(key + "=") == 0) {
                                    array[i] = key + "=" + val;
                                    addnew = false;
                                    break;
                                }
                            }
                            if (addnew) {
                                array.push(key + "=" + val);
                            }
                            storage.setItem(name, array.join(";"));
                        }
                        return true;
                    },
                    Get: function(name, key) {
                        var backval = "";
                        var loadval = storage.getItem(name);
                        if (loadval != undefined && loadval != null) {
                            var array = loadval.split(";");
                            var len = array.length;
                            for (var i = 0; i < len; i++) {
                                if (array[i].indexOf(key + "=") == 0) {
                                    backval = array[i].substr(array[i].indexOf("=") + 1);
                                    break;
                                }
                            }
                        }
                        return backval;
                    },
                    Del: function(name, key) {
                        return this.Set(name, key, "");
                    },
                    Remove: function(name) {
                        if (storage.getItem(name)) {
                            storage.setItem(name, "");
                        }
                        return true;
                    }
                }
            })();

            //userData����ie6/7
            var u = (function() {
                if (o.u) document.documentElement.addBehavior("#default#userdata");
                return {
                    Set: function(name, key, val) {
                        with (document.documentElement) try {
                            load(name);
                            setAttribute(key, val);
                            save(name);
                            return true;
                        }
                        catch (e) {
                            return false;
                        }
                    },
                    Get: function(name, key) {
                        var backval = "";
                        with (document.documentElement) try {
                            load(name);
                            backval = getAttribute(key);
                            if (backval == undefined || backval == null) { backval = ""; }
                        } catch (e) { }
                        return backval;
                    },
                    Del: function(name, key) {
                        return this.Set(name, key, "");
                    },
                    Remove: function(name) {
                        with (document.documentElement) try {
                            load(name);
                            expires = new Date(315532799000).toUTCString();
                            save(name);
                            return true;
                        }
                        catch (e) { return false; }
                    }
                }
            })();

            //Public����
            return {
                Set: function(name, key, val) {
                    for (var s in o) {
                        if (eval("o." + s)) {
                            return (eval(s + ".Set(\"" + name + "\",\"" + key + "\",\"" + encodeURIComponent(val) + "\")"));
                        }
                    }
                },
                Get: function(name, key) {
                    for (var s in o) {
                        if (eval("o." + s)) {
                            return decodeURIComponent(eval(s + ".Get(\"" + name + "\",\"" + key + "\")"));
                        }
                    }
                },
                Del: function(name, key) {
                    for (var s in o) {
                        if (eval("o." + s)) {
                            return (eval(s + ".Del(\"" + name + "\",\"" + key + "\")"));
                        }
                    }
                },
                Remove: function(name) {
                    for (var s in o) {
                        if (eval("o." + s)) {
                            return (eval(s + ".Remove(\"" + name + "\")"));
                        }
                    }
                },
                IsReady: function() {
                    for (var s in o) {
                        if (eval("o." + s)) {
                            if (s == "f") {//flash
                                return f.IsReady();
                            } else {
                                return true;
                            }
                            //return (eval(s + ".IsReady()"));
                        }
                    }
                }
            }
        })();
    })();