using C=System.Console;
class Z
{
static void Main()
{
var i=R().Split(' ');
int l=P(i[0]),m=P(i[1]),t=P(i[2]),u=P(i[3]);
w:
R();
if(l>t&&m>u){M("SE");t++;u++;}
if(l>t&&m==u){M("E");t++;}
if(l>t&&m<u){M("NE");t++;u--;}
if(l==t&&m>u){M("S");u++;}
if(l==t&&m<u){M("N");u--;}
if(l<t&&m==u){M("W");t--;}
if(l<t&&m>u){M("SW");t--;u++;}
if(l<t&&m<u){M("NW");t--;u--;}
goto w;
}
static void M(string d)=>C.WriteLine(d);
static string R()=>C.ReadLine();
static int P(string v)=>int.Parse(v);
}