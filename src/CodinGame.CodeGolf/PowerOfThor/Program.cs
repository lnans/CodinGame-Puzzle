using C=System.Console;
class Z
{
static void Main()
{
var i=C.ReadLine().Split(' ');
int l=P(i[0]),m=P(i[1]),t=P(i[2]),u=P(i[3]),q=t-l,p=u-m;
w:
C.ReadLine();
string y="",z="";
if(p>0){y="N";p--;}
if(p<0){y="S";p++;}
if(q>0){z="W";q--;}
if(q<0){z="E";q++;}
C.WriteLine(y+z);
goto w;
}
static int P(string v)=>int.Parse(v);
}