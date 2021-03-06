﻿// /*
// * Copyright (c) 2016, Alachisoft. All Rights Reserved.
// *
// * Licensed under the Apache License, Version 2.0 (the "License");
// * you may not use this file except in compliance with the License.
// * You may obtain a copy of the License at
// *
// * http://www.apache.org/licenses/LICENSE-2.0
// *
// * Unless required by applicable law or agreed to in writing, software
// * distributed under the License is distributed on an "AS IS" BASIS,
// * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// * See the License for the specific language governing permissions and
// * limitations under the License.
// */
using System.Collections.Generic;
using Alachisoft.NosDB.Distributor.DataCombiners;
using Alachisoft.NosDB.Distributor.DistributedDataSets;

namespace Alachisoft.NosDB.Distributor.DataSelectors
{
    public class DataSelectorLimit : IDataSelector
    {
        private IDataSelector _dataSelector;
        private long _limit;
        private ISetElement _current = null;

        public DataSelectorLimit(IDataSelector dataSelector, long limit)
        {
            _dataSelector = dataSelector;
            _limit = limit;
        }

        #region IDataSelector Methods
        public void Initialize(IList<ISet> sets, List<IDataCombiner> combiners, System.Collections.IComparer comparer)
        {
            _dataSelector.Initialize(sets, combiners, comparer);
        }

        public object Current
        {
            get { return _current; }
        }

        public bool MoveNext()
        {
            if (_limit > 0)
            {
                if(_dataSelector.MoveNext())
                {
                    _current = (ISetElement)_dataSelector.Current;
                    _limit--;
                    return true;
                }
            }
            _current = null;
            return false;
        }

        public void Reset()
        {
            _limit = 0;
            _dataSelector.Reset();
        }
        #endregion
    }
}
