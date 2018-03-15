﻿namespace Tangle.Net.Mam.Unit.Tests.Mam
{
  using System.Configuration;

  using Microsoft.VisualStudio.TestTools.UnitTesting;

  using Tangle.Net.Cryptography;
  using Tangle.Net.Entity;
  using Tangle.Net.Mam.Mam;
  using Tangle.Net.Mam.Merkle;
  using Tangle.Net.Utils;

  /// <summary>
  /// The mam channel test.
  /// </summary>
  [TestClass]
  public class MamChannelTest
  {
    /// <summary>
    /// The test public channel creation.
    /// </summary>
    [TestMethod]
    public void TestPublicChannelCreation()
    {
      var seed = new Seed("JETCPWLCYRM9XYQMMZIFZLDBZZEWRMRVGWGGNCUH9LFNEHKEMLXAVEOFFVOATCNKVKELNQFAGOVUNWEJI");
      var mamFactory = new CurlMamFactory(new Curl(), new CurlMask());
      var treeFactory = new CurlMerkleTreeFactory(new CurlMerkleNodeFactory(new Curl()), new CurlMerkleLeafFactory(new AddressGenerator(seed)));

      var channelFactory = new MamChannelFactory(mamFactory, treeFactory);
      var channel = channelFactory.Create(MamMode.Public, seed);

      Assert.AreEqual(seed.Value, channel.Seed.Value);
      Assert.AreEqual(MamMode.Public, channel.Mode);
      Assert.AreEqual(SecurityLevel.Medium, channel.SecurityLevel);
    }

    /// <summary>
    /// The test restricted channel creation.
    /// </summary>
    [TestMethod]
    public void TestRestrictedChannelCreation()
    {
      var seed = new Seed("JETCPWLCYRM9XYQMMZIFZLDBZZEWRMRVGWGGNCUH9LFNEHKEMLXAVEOFFVOATCNKVKELNQFAGOVUNWEJI");
      var mamFactory = new CurlMamFactory(new Curl(), new CurlMask());
      var treeFactory = new CurlMerkleTreeFactory(new CurlMerkleNodeFactory(new Curl()), new CurlMerkleLeafFactory(new AddressGenerator(seed)));

      var channelFactory = new MamChannelFactory(mamFactory, treeFactory);
      var channelKey = new TryteString("NXRZEZIKWGKIYDPVBRKWLYTWLUVSDLDCHVVSVIWDCIUZRAKPJUIABQDZBV9EGTJWUFTIGAUT9STIENCBC");
      var channel = channelFactory.Create(MamMode.Restricted, seed, SecurityLevel.Medium, channelKey);

      Assert.AreEqual(seed.Value, channel.Seed.Value);
      Assert.AreEqual(MamMode.Restricted, channel.Mode);
      Assert.AreEqual(SecurityLevel.Medium, channel.SecurityLevel);
      Assert.AreEqual(channelKey.Value, channel.Key.Value);
      Assert.AreEqual(Hash.Empty.Value, channel.NextRoot.Value);
      Assert.AreEqual(0, channel.Index);
      Assert.AreEqual(0, channel.Start);
      Assert.AreEqual(1, channel.Count);
      Assert.AreEqual(1, channel.NextCount);
    }

    /// <summary>
    /// The test restricted message creation.
    /// </summary>
    [TestMethod]
    public void TestRestrictedMessageCreation()
    {
      var bundle = new Bundle();
      bundle.AddTransfer(new Transfer
                           {
                             Address = new Address(),
                             Message = new TryteString("AQRAQLYEXHTXQUVYAXBDJZFWM9QPHXNQRVVGEODVNZAQMPXIHVYDFLHKDBFLSEUDGHVGNYFLEBQTJORJ9BDWXYUBYQDBKYHXGCIRVRJLLRQCBFSYYFTRVRPYJTHIIOOFDISBILGHQCWXSNLRXRKPTBSBO9ENPHHCSTPGFEVK9GVOWCBTJKVRANFBPHEOQNTJJQNYWQSYYRXZMFHADWUQJVBOLP9BUV9PXUIAKDUGRICOLQXXSXNGNMUMBWJRJDBSUJWSPGLXMHBLUUSSWSQUTSWKRRPWURYJYHRHSRCLBEBMHKVFJMLYDDEIHHBCAZYLLNGMFTUSUZA9VAWSSFSKLDIFYDDZJXJZ9VRIADVEAPMRBBUAOJCWCWMBUEVGBFMXPHQEE9JNPLLSDBDJSLXZXISGDOAMGQSVMOMKOWTWJPYCDKXRRBBKLPJRJMN9LJZQZADQQYXNBNFQDBITXBXPSMWDNNJAODPSEJRQZCKTLSOLUIMOWXZHYXJOXAGRJC9NNGWLYY9VQYW9IYFCQWDZQJDNWHVIXEIYSHTSOGRHMPQZ9YUTWDCRUEZGJTKVDRFNZVRBVAJBBNHILDFSBPNJKBUVXW9CDOEGVFPAWUZROYEBYT9NHI9DNGLHWPSKMJELJSMVTRNGEJNAMNEWUQ9GSRANMRIEB9GWXJD9GX9EHTRNYEYZJFKALYJVPUITHKGMM9QJDOKHWXQ9YN9ZBBCRNESWWBPWZIQVGWDQSRVROIRTPK9STFWRDDUFDKSLQQOXRWSSQCOHIJUJLPOBUVNWLOGGXDMEFOQHXSGQOZVQGYLUABLBJHXZRRIB9ZTLAXHHSRHAODYTSDTJUGRWVDJAJNRNDL9YVEPKLVAQFPAONMKOHHCLAZWFTAZTMEQKGNMNBYHBGNNOV99HCMQDRZEGGPAWA9FFO9SESTP9TSFXLCYPJJ9JKROY99VVHENCHSSSXKDAFRGOKAKIWCOHXYNFPKKWCBSGENRBX9FQFBITIO9999USHGAMQMBYPYPQANDGZXIPYVFZCTRGALNR9XGZBYAKQYIJDSBTWHIJQMOVOVBVCRUXZD9IGMODZPXLGTKYOWRVQC9YYVVBVMDRSUUPJDJUMLNCPGZJLTGQPQOXKGKUWASQQ9AER9JLWMQA9GLPATMVRDFIWLC9GBIKRXGJSZLPZVKGTWIIDCGXIDKMLQTCWTPICN9UZZXEOFFCQSJPOXFSCEHJNGIGOYIFZFMPWHJOFZGIYRFWOZCSSHKPWHDEELMN9ZWULHRMRHVNGTNUEZEMRINSVYEGBOJMNYFWNLFSIHHRIFXZRLEGFIEOT9WYZQXFTKXRAYRRKDV9VYUBSZAKARAIZYDWBEDLLOCAXQZIXSIRFGCGZWVRRCME9TXPTDUYLYUGLIBENQ99SDQLYFBJZHGQYQBLJMLFZLQLPHYMCFMTOSDA99DXAMCEAMBGNFFDJIKTUCBHSYKH9KLRVBYXOJJYKZYXJLIKIZFQHNFWWNCQACXZIXDLJXNU9UNZQJMFLJJXGSEPXIFRDKSUENGGELMFPVQKHAYPHMIPHVKZTNFTWMERRTVFHJLKILHRX9XSVYJZQKQXIMIMPDGCNXZKXUBNZOVXMCZXGXZTVBTIQUZ9GDQJGIHFKFXYZHDNGPIUXLIKSDBQQNMJCVRJUKQVJWGOFOBTXDSFLM9BGYZJREIBUBEVCOME9NNKHFEKWGJRTMZFDHFZY9YHTFJNCNZBGMVEEKUYPZBYQQFVVZEFSQDXOWTNUSU9GUTS9ITJAYYELPZYITWELVOAKMPTUIL9UJD9TN9HVLIYWAHMQBIHLUSBRSAGXQRAOOKGIBYMM9AOLC9DQVMNFMPGDSQUTWMRKVKE9FRZNCQJDVDKBINILRFKBEEDZTWI9UUCNLIMT9MLQRMBRJQLKHJXEKWJDEK9WAKXUJVWMUNLBUSVYSUZDOIEJAKZZOWPQFNKDLPDICGTQKOSBPWKHGPBUWYXWYVPIOIELDLIBPI9FJRSHVEKRNMQ9VPOCCCASWKL99TZCFNHLHVMOOFNBGSRXYKCKJOLNEBYAFCRMDHOVWUZ9MIFDSRRFSHSIWIN9WLLXMOKCXYTTESFZUNMQAHYVOVQQH9YZVOQATUPEPGCCGTPBAAOSPJ9ALPYXONR9OCODZDJPNPMQIREEXICFQIWRZ99OPS9ALP9GLPTTWGRJHATSJJTEKFZDJYEUKBIAACLOALTSXWCCL9GBUTWIXVEW9HYLVKRRRAATNYMGSAKLUN9HJQPIMXJOYIDXNONMHXZUGXOFIQIFBTPYDXXMKOAUUNOJU9IIXFLSSWKOCRJKXNWKKAMJRMFYSMAOWSCI9NTRQKT99CIDHJMZWSDKAIKOYWHSCJZRQWWDZOC9TKXHXCDN9GTMMGXXHXIJFMTCQFTDAKPEPQLNVCIE9UMIYXHDQUGPJSHPMRFCHMPGOYHVATXBIGOCZAFDNTVMJRJIVQDLNQYNSASMLXPSFUMYBGHXTCFYDLOGOCDTWPZMVPKMP9EEXEKNMSTEQKFZIVU9DKHSEQA9XQNOKUK9ABSTZ9XRPDMDECMYRRGSYHEZDAWEDGTQSMRC9DQ9FUWS9QELTDYZCRFYABZKVLCFGVKYGYIWJGNQKI9ONMXWFJNFIEPZRSSLNUUQOXK9YXQYMWR9B9UAUXYYONKYUG9JAJOUMLKQOXBD9JBLAQHXBJINNLBXKYGYLPRLASOFISEFXG9KENN9YFFP9J9NVVYKAEFAUTIQRDTVKVHQYEWJOGQEUPSWEIMYWYYEZRGJUEDAHDYPQEGX9KTUODLORTCJZGJCKFGEJTWRGEACRRINEQBUWS9NUPP9BMONGQQABVGBLRQXZJYXDHS9DFWAWLOZVKSDZD9UGBPKDCHLYEAUOABXKMNWTSUZTHTZBYOALUHQHSEK9WPGWGVNQEGVDDFZJXLAYINRUQYQBROPB9RGAFWMPOBOANCGW9DJJ9NUIHLQE9RNZOHLPYAIACONJXYFRBAOVJEWOTO9HZCZOVJGHQOGGJAB9QUWDRI9KLX9EGOHGTJIYQHTTMAAETKGBLDQXGRTJOEHRIAPXACPFTVGIXUJFZXUCX9EHUESMGQN9EHXEQTSKJHWDIR9OJYP9USDPWECONYRTFZREIZHOBAOXK9FCEOWCLUNKEWKE9JPQGDZ9PWAVQUMNBLVMESODMGJAIVNVJSFTQFL9RTTCJAFIMIGXASIREOXLNVEHMHPQZIKIPRQWFGYLQHVPMBNIWTWINSKW9WIWVDKHAIHNMBOCETYLNTJMA9XMEVQBPUFPKXJHXHPQCFKF9UYQQFKBBPPZMNAKPMMHPLBPDAOACLZ9HKVFGGMQHHHQLWYWYYGNXPEITKTVNURENFBAOBXITZDYCUSASIYEVJBYDLBIMUF9HILXKYH9IDYOFEQMBHJJBFDTNPDXSNSXCPUFGRIOXNYOSKHBILRWEFWZELL9GLNQOMPLMAPTOIJXSQXNPIW9XLMYEYVTNM9BVLVAIPZSYTCHHXGBQXHCBOQAUHZXKDWUREOOGKK9S9ZTKHGIQWHZOOFCGXBBCSYPGOGNRJLGVNWAGJZWKKLTHCJG9FJVRXMHQARRAHGUTITDDIMTIZAYARKJMQFCEAAOTHSHXDQKSNGMN99YGWRNLNMJXXFIQD9THOIHINYIEJMWRCZTAHJC9QIAKDIHIELNIMDKHFFWVCAHGMITJYNZ9XZSLFCGFFEQLONULNTQAJLOJTWO9BJQEBGTY9OCJBXKXNDEJWCDRYCNFDKYODHPAHQBGDXHMLVETVNRLRIJDHKAHZBAOFGMQFSTHPHBXKRAJFVIUDCKGNCDD9XTFDVNW9RLRNN9QRRUGOTMRRSVISCWE9ALTXCOCYHIQGXLGDB9RGICVSTUHJFWMWQCUICLJZCACGYCUHMGTGEJGAACGEXKJNJWKUGXRNBTEKIYUNXZZHXASCBPRDJAVXWESHU9DW9UVPJLWBKUXPDAIQ9JQPAHPMGVRUY9RLAMRTOKDVVUAZAPMNBSHRNNYDGC9BKAKKBPGHFDOHXCFWNSH9FZUMQHXMIUYKQLKHAYXPNEDYDUXTZXUKTBRJGBSZFMCNRZIQQLOLPRZPZBCX9EZBRUFGHGHBLYBLYYWZACGWGYRGV9IGZLTXOIMSCOTBNIGUXMOPVOFKIEUZASTZAHKXLYECJFLVZCORRFYDNVCYSKFCREQXXMDEHPSQFDERBICWFMOJS9PPSQYTVEXYSYGHWIWITZHRBZLHJWUYWQCAPLSBYJTJGCUXQPITAUJX9WEUVJDNUNKSDLHWDWKDQARSRKSLGLUCDNCERMKOJXCRXWCCYIAQZVGJLYTYQNGUDYYMXXL9XDBLEWPRMAY9DMMUHYUPMNBRRXJDIFDWEFJBRPYMPDPIPGXGXBMZ9JHAOISUMCNGUNCCPWKFJNBR9ZATRMDTOFDKMBQEZSCDSBNNEIUUCDHPHFBOBXLNGPOVZCATQRKVYSQHSHIVFZTW9BHPTPXUJGNNUBHWLZF9TBA9JONTWZRWHGHFIQGHAKSWBAHB99"),
                             Tag = new Tag("ASDF"),
                             Timestamp = Timestamp.UnixSecondsTimestamp
                           });

      bundle.Finalize();
      bundle.Sign();

      var seed = new Seed("JETCPWLCYRM9XYQMMZIFZLDBZZEWRMRVGWGGNCUH9LFNEHKEMLXAVEOFFVOATCNKVKELNQFAGOVUNWEJI");  
      var mamFactory = new CurlMamFactory(new Curl(), new CurlMask());
      var treeFactory = new CurlMerkleTreeFactory(new CurlMerkleNodeFactory(new Curl()), new CurlMerkleLeafFactory(new AddressGenerator(seed)));

      var channelFactory = new MamChannelFactory(mamFactory, treeFactory);
      var channelKey = new TryteString("NXRZEZIKWGKIYDPVBRKWLYTWLUVSDLDCHVVSVIWDCIUZRAKPJUIABQDZBV9EGTJWUFTIGAUT9STIENCBC");
      var channel = channelFactory.Create(MamMode.Restricted, seed, SecurityLevel.Medium, channelKey);

      var message = channel.CreateMessage(new TryteString("IREALLYWANTTHISTOWORKINCSHARPASWELLPLEASEMAKEITHAPPEN"));

      Assert.AreEqual("RRPXQHDJY9BKXC9NGHDCSHRIDYORSUUEPFHXPQVDGSQTVYPCGVIZRWQINOUYFDUXTHFTKHLBOLYLHMKE9", message.Root.Value);
      Assert.AreEqual("BAVSMNXFTVBBEPXVROQYWBFHAELANDS9UFLDEOERJGKMXOGTL9UBEJF9WUDNGKUEDFZYAAFACRRRACDHV", message.Address.Value);
      Assert.AreEqual("OLHRFQPHPPQWTVSZNIZEKFOB9JPWKWQQPUCNLFAVEYCL9QVXRWFTDT9KPIHERRULOOBUKTJZJWKENTPLO", message.NextRoot.Value);

      Assert.AreEqual("OLHRFQPHPPQWTVSZNIZEKFOB9JPWKWQQPUCNLFAVEYCL9QVXRWFTDT9KPIHERRULOOBUKTJZJWKENTPLO", channel.NextRoot.Value);

      for (var i = 0; i < bundle.Transactions.Count; i++)
      {
        Assert.AreEqual(bundle.Transactions[i].Fragment.Value, message.Payload.Transactions[i].Fragment.Value);
      }
    }
  }
}