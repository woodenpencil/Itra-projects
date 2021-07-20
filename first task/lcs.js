function algo(strings){
    var id_min = 0;
    for (var i = 0; i < strings.length; i++) {
        if(strings[i].length < strings[id_min].length){
            id_min = i;
        }
    }
    for(var l = strings[id_min].length;l>0;l--){
        
        for(var s_beg = 0;s_beg<=strings[id_min].length-l;s_beg++){
            const sub = strings[id_min].slice(s_beg, s_beg+l);
            
            var found = true;
            for(var i=0;i<strings.length;i++){
                if(i==id_min)
                    continue;
                if(strings[i].indexOf(sub)<0){
                    found = false;
                    break;
                }
            }
            if(found){
                console.log(`${sub}\n`);
                return;
            }

        }
    }
    console.log('\n');
    return;
}
if(process.argv.length<3)
    console.log('\n');
else
    algo(process.argv.slice(2))